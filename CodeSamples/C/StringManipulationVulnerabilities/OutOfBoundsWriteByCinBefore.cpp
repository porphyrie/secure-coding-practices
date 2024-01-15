#include <iostream>

int main(void)
{
    char buf[12];
    std::cout << "write buf: ";
    std::cin >> buf;
    std::cout << "echo: " << buf << '\n';
}

