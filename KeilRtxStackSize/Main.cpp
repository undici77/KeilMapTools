// This file is part of KeilMapLib.
//
// KeilMapLib is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// KeilMapLib is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License
// along with KeilMapLib.  If not, see <https://www.gnu.org/licenses/>.

#include "stdafx.h"
#include "GeneratorFactory.h"
#include "Main.h"

/******************************************************************************/

const std::string App_Name    = "KeilRtxStackSize";
const std::string App_Version = "v1.0.0";
const std::string App_Author  = "Alessandro Barbieri";

/******************************************************************************/
void split(const std::string &input, char delimiter, std::vector<std::string> &output)
/******************************************************************************/
{
	std::stringstream string_stream(input);
	std::string       token;

	while (std::getline(string_stream, token, delimiter))
	{
		output.push_back(token);
	}
}

/******************************************************************************/
void read_ini(const boost::filesystem::path &ini_file_path, PARAMETERS *parameters)
/******************************************************************************/
{
	boost::property_tree::ptree property_tree;
	std::string                 message;
	std::string                 buffer;
	std::string                 key;
	boost::filesystem::path     path;

	try
	{
		boost::property_tree::ini_parser::read_ini(ini_file_path.string(), property_tree);
	}
	catch (...)
	{
		boost::property_tree::ini_parser::write_ini (ini_file_path.string(), property_tree);
		boost::property_tree::ini_parser::read_ini (ini_file_path.string(), property_tree);
	}

	// Architecture.Name
	try
	{
		parameters->architecture = property_tree.get<std::string>("Architecture.Name");
		if (parameters->architecture.empty())
		{
			parameters->architecture = "STM32";
			property_tree.put<std::string>("Architecture.Name", parameters->architecture);
		}
	}
	catch (...)
	{
		parameters->architecture = "STM32";
		property_tree.put<std::string>("Architecture.Name", parameters->architecture);
	}

	// Map.FilePath
	try
	{
		buffer = property_tree.get<std::string>("Map.FilePath");
		if (buffer.empty())
		{
	    	parameters->map_file_path = boost::filesystem::absolute(ini_file_path.parent_path());
    		parameters->map_file_path.append("Application.map");
			path = boost::filesystem::relative(parameters->map_file_path.string());
			property_tree.put<std::string>("Map.FilePath", path.string());
		}
		else
		{
            boost::algorithm::trim_if(buffer, boost::is_any_of("\""));
			parameters->map_file_path = boost::filesystem::absolute(buffer);
			path = boost::filesystem::relative(parameters->map_file_path.string());
			property_tree.put<std::string>("Map.FilePath", path.string());
		}
	}
	catch (...)
	{
    	parameters->map_file_path = boost::filesystem::absolute(ini_file_path.parent_path());
   		parameters->map_file_path.append("Application.map");
		path = boost::filesystem::relative(parameters->map_file_path.string(), boost::filesystem::current_path());
		property_tree.put<std::string>("Map.FilePath", path.string());
	}

	// Output.FilePath
	try
	{
		buffer = property_tree.get<std::string>("Output.FilePath");
		if (buffer.empty())
		{
			parameters->output_file_path = boost::filesystem::current_path();
			parameters->output_file_path.append("Src");
			parameters->output_file_path.append("os_threads_stack_size.h");

			path = boost::filesystem::relative(parameters->output_file_path.string(), boost::filesystem::current_path());
			property_tree.put<std::string>("Output.FilePath", path.string());
		}
		else
		{
            boost::algorithm::trim_if(buffer, boost::is_any_of("\""));
			parameters->output_file_path = boost::filesystem::absolute(buffer);
		}
	}
	catch (...)
	{
		parameters->output_file_path = boost::filesystem::current_path();
		parameters->output_file_path.append("Src");
		parameters->output_file_path.append("os_threads_stack_size.h");

		path = boost::filesystem::relative(parameters->output_file_path.string(), boost::filesystem::current_path());
		property_tree.put<std::string>("Output.FilePath", path.string());
	}

	// Threads.SubString
	try
	{
		buffer = property_tree.get<std::string>("Threads.Patterns");
		if (buffer == "")
		{
			buffer = "thread,task";
			property_tree.put<std::string>("Threads.Patterns", buffer);
		}

		split(buffer, ',', parameters->thread_patterns);
	}
	catch (...)
	{
		buffer = "thread,task";
		split(buffer, ',', parameters->thread_patterns);
		property_tree.put<std::string>("Threads.Patterns", buffer);
	}

	// Stack.Oversizing
	try
	{
		parameters->stack_oversizing = property_tree.get<size_t>("Stack.Oversizing");
	}
	catch (...)
	{
		parameters->stack_oversizing = 0;
		property_tree.put<size_t>("Stack.Oversizing", parameters->stack_oversizing);
	}

	boost::property_tree::ini_parser::write_ini (ini_file_path.string(), property_tree);
}

/*****************************************************************************/
int main(int argc, char *argv[])
/*****************************************************************************/
{
	boost::filesystem::path         ini_file_path;
	PARAMETERS                      parameters;
	std::unique_ptr<GeneratorBase>  generator;
	std::string                     buffer;
	std::string                     error;
	int                             result;

	result = 1;

	try
	{
		boost::program_options::options_description description(App_Name + " " + App_Version + " options");
		boost::program_options::variables_map       variable_map;

		description.add_options()
		("help",                                                    "produce help message")
		("ini",       boost::program_options::value<std::string>(), "set ini configuration file")
		("debug",     boost::program_options::value<std::string>(), "enable debug info")
		;

		boost::program_options::store(boost::program_options::parse_command_line(argc, argv, description), variable_map);
		boost::program_options::notify(variable_map);

		if (variable_map.count("help"))
		{
			std::cout << description << std::endl;
			return (EXIT_FAILURE);
		}

		if (variable_map.count("ini"))
		{
            buffer = variable_map["ini"].as<std::string>();
            boost::algorithm::trim_if(buffer, boost::is_any_of("\""));
			ini_file_path = boost::filesystem::absolute(buffer);
			if (!boost::filesystem::is_regular_file(ini_file_path))
			{
				ini_file_path.append("KeilRtxStackSize.ini");
			}

			read_ini(ini_file_path, &parameters);
		}
		else
		{
			std::cout << description << std::endl;
			return (EXIT_FAILURE);
		}

		std::cout << App_Name << " " << App_Version << " by " << App_Author << std::endl;

		if (variable_map.count("debug"))
        {
		    std::cout << "    Architecture: " << parameters.architecture     << std::endl;
		    std::cout << "    Map file:     " << parameters.map_file_path    << std::endl;
		    std::cout << "    Output file:  " << parameters.output_file_path << std::endl;
		    std::cout << "    Oversizing:   " << parameters.stack_oversizing << std::endl;
        }

		generator = GeneratorFactory::Make(parameters.architecture);
		if (generator == NULL)
		{
			std::cout << "Error: architecture not found" << std::endl;
			return (EXIT_FAILURE);
		}

		if (!generator->Read(parameters, &error))
		{
			std::cout << "Error: " << error << std::endl;
			return (EXIT_FAILURE);
		}

		generator->Generate(parameters);

		if (!generator->WriteRequest())
		{
			std::cout << parameters.output_file_path << " OK!" << std::endl;
			return (EXIT_SUCCESS);
		}

		if (!generator->Write(parameters))
		{
			std::cout << "Error: unable to write " << parameters.output_file_path << std::endl;
			return (EXIT_FAILURE);
		}

		std::cout << "Error: " << parameters.output_file_path << " !!PLEASE REBUILD PROJECT!!" << std::endl;
		return (EXIT_FAILURE);
	}
	catch (std::exception &e)
	{
		std::cerr << "Error: " << e.what() << std::endl;

		return (EXIT_FAILURE);
	}
	catch (...)
	{
		std::cerr << "Exception of unknown type!" << std::endl;
		return (EXIT_FAILURE);
	}
}

