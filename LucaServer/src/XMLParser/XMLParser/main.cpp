#include <iostream>
#include <fstream>

int main()
{
	std::fstream file;
	std::string text = "Hello, world";
	
	file.open("settings.xml");
	
	if (file.is_open())
	{
		// Write data to file.
		file << text.c_str();
		file.close();
		return 0;
	}
	else
	{
		std::cout << "Error opening file.";
		return 1;
	}
}