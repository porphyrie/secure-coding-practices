#include <iostream>
#include <string>

using namespace std;

int main(void)
{
    string buf;
    std::cout << "write buf: ";
    std::cin >> buf;
    std::cout << "echo: " << buf << '\n';
    std::cout << "write buf: ";
    std::cin >> buf;
    std::cout << "echo: " << buf << '\n';
}

