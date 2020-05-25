#include <iostream>
#include <WS2tcpip.h>

// Link Ws2_32.lib.
#pragma comment (lib, "Ws2_32.lib")

#define PORT "1234"
#define DEFAULT_BUFLEN 512

int main()
{
	std::cout << "STARTING SERVER..." << std::endl;

	WSADATA wsaData;
	int iResult;

	SOCKET ListenSocket = INVALID_SOCKET;
	SOCKET ClientSocket = INVALID_SOCKET;

	struct addrinfo *result = NULL;
	struct addrinfo hints;

	int iSendResult;
	char recvbuf[DEFAULT_BUFLEN];
	int recvbuflen = DEFAULT_BUFLEN;

	// Initialize Winsock.
	std::cout << "WINSOCK ";
	iResult = WSAStartup(MAKEWORD(2, 2), &wsaData);
	if (iResult != 0)
	{
		std::cout << "ERROR: " << iResult << std::endl;
		return 1;
	}
	else
		std::cout << "OK" << std::endl;

	ZeroMemory(&hints, sizeof(hints));
	hints.ai_family = AF_INET;
	hints.ai_socktype = SOCK_STREAM;
	hints.ai_protocol = IPPROTO_TCP;
	hints.ai_flags = AI_PASSIVE;

	// Resolve the server address and port (host).
	std::cout << "RESOLVE HOST ";
	iResult = getaddrinfo(NULL, PORT, &hints, &result);
	if (iResult != 0)
	{
		std::cout << "ERROR: " << iResult << std::endl;
	}
	else
		std::cout << "OK" << std::endl;

	// Create a SOCKET for connecting to server.
	std::cout << "CREATE SOCKET ";
	ListenSocket = socket(result->ai_family, result->ai_socktype, result->ai_protocol);
	if (ListenSocket == INVALID_SOCKET)
	{
		std::cout << "ERROR: " << WSAGetLastError() << std::endl;
		freeaddrinfo(result);
		WSACleanup();
		return 1;
	}
	else
		std::cout << "OK" << std::endl;

	// Setup the TCP listening socket.
	std::cout << "BIND SOCKET ";
	iResult = bind(ListenSocket, result->ai_addr, (int)result->ai_addrlen);
	if (iResult == SOCKET_ERROR)
	{
		std::cout << "ERROR: " << WSAGetLastError() << std::endl;;
		freeaddrinfo(result);
		closesocket(ListenSocket);
		WSACleanup();
		return 1;
	}
	else
		std::cout << "OK" << std::endl;

	freeaddrinfo(result);

	std::cout << "SET LISTENER ";
	iResult = listen(ListenSocket, SOMAXCONN);
	if (iResult == SOCKET_ERROR)
	{
		std::cout << "ERROR: " << WSAGetLastError() << std::endl;
		closesocket(ListenSocket);
		WSACleanup();
		return 1;
	}
	else
		std::cout << "OK" << std::endl;

	// Accept the cliet socket.
	std::cout << "ACCEPT ";
	ClientSocket = accept(ListenSocket, NULL, NULL);
	if (ClientSocket == INVALID_SOCKET)
	{
		std::cout << "ERROR: " << WSAGetLastError() << std::endl;
		closesocket(ListenSocket);
		WSACleanup();
		return 1;
	}
	else
		std::cout << "OK" << std::endl;

	// No longer need server socket, yeet it.
	closesocket(ListenSocket);

	std::cout << "SERVER OK" << std::endl;

	// Receive until the peer shuts down the connection.
	do {
		iResult = recv(ClientSocket, recvbuf, recvbuflen, 0);
		if (iResult > 0)
		{
			std::cout << "<client> " << iResult << std::endl;

			// Echo result back to sender.
			std::cout << "ECHO ";
			iSendResult = send(ClientSocket, recvbuf, iResult, 0);
			if (iSendResult == SOCKET_ERROR)
			{
				std::cout << "ERROR: " << WSAGetLastError() << std::endl;
				WSACleanup();
				return 1;
			}
			std::cout << "OK" << std::endl;
		}
		else if (iResult == 0)
			std::cout << "CLIENT DISCONNECTED" << std::endl;
		else 
		{
			std::cout << "RECEIVE FAILED" << WSAGetLastError() << std::endl;
			closesocket(ClientSocket);
			WSACleanup();
			return 1;
		}
	} while (iResult > 0);

	// Shutdown connection.
	std::cout << "SHUTDOWN ";
	iResult = shutdown(ClientSocket, SD_SEND);
	if (iResult == SOCKET_ERROR)
	{
		std::cout << "ERROR: I'm sorry, Dave. I'm afraid I can't do that." << std::endl;
		closesocket(ClientSocket);
		WSACleanup();
		return 1;
	}

	// Cleanup.
	closesocket(ClientSocket);
	WSACleanup();

	std::cout << "OK" << std::endl;
	return 0;
}