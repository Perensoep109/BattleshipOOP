#include <iostream>
#include <sstream>
#include <WS2tcpip.h>
#include <vector>

class Log
{
private:
	bool m_traceEnabled = false;
	bool m_warningEnabled = false;
	bool m_errorEnabled = false;
	bool m_fatalEnabled = false;
	bool m_verboseEnabled = false;

public:
	HANDLE hConsole = GetStdHandle(STD_OUTPUT_HANDLE);

	void SetLogLevel(const char* level)
	{
		std::string enteredLevel = level;
		
		for (int i = 0; i < sizeof(enteredLevel); i++)
		{
			enteredLevel[i] = tolower(enteredLevel[i]);
		}

		if (enteredLevel.find("none") != std::string::npos)		{ return; }
		if (enteredLevel.find("trace") != std::string::npos)	{ m_traceEnabled = true; }
		if (enteredLevel.find("warning") != std::string::npos)	{ m_warningEnabled = true; }
		if (enteredLevel.find("error") != std::string::npos)	{ m_errorEnabled = true; }
		if (enteredLevel.find("fatal") != std::string::npos)	{ m_fatalEnabled = true; }
		if (enteredLevel.find("verbose") != std::string::npos)	{ m_verboseEnabled = true; }
	}


	void Trace(const char* message)
	{
		if (m_traceEnabled || m_verboseEnabled)
		{
			SetConsoleTextAttribute(hConsole, 10);
			std::cout << "[INFO]: " << message << std::endl;
			SetConsoleTextAttribute(hConsole, 7);
		}
	}

	void Warning(const char* message)
	{
		if (m_warningEnabled || m_verboseEnabled)
		{
			SetConsoleTextAttribute(hConsole, 14);
			std::cout << "[WARNING]: " << message << std::endl;
			SetConsoleTextAttribute(hConsole, 7);
		}
	}

	void Error(const char* message)
	{
		if (m_errorEnabled || m_verboseEnabled)
		{
			SetConsoleTextAttribute(hConsole, 13);
			std::cout << "[ERROR]: " << message << std::endl;
			SetConsoleTextAttribute(hConsole, 7);
		}
	}
	
	void Fatal(const char* message)
	{
		if (m_fatalEnabled || m_verboseEnabled)
		{
			SetConsoleTextAttribute(hConsole, 12);
			std::cout << "[FATAL]: " << message << std::endl;
			SetConsoleTextAttribute(hConsole, 7);
		}
	}
};

class Player
{
public:
	int boardX;
	int boardY;

	// For now, work only with a 10x10 game board.
	int gameBoard[100];

	void getBoard(int receivedGameBoard[])
	{
		// Store the copy of the player's game board you received from the client.
		for (int i = 0; i < sizeof(receivedGameBoard); i++)
		{
			gameBoard[i] = receivedGameBoard[i];
		}
	}

	std::vector<int> getBoat(/*data from boat packet*/)
	{

	}
};

class Game
{

};

void Parse(char* data)
{
	Log log;

	char* command;
	command = data;



	char packetSize = command[0];
	char packetType = command[1];
	char gameID[4] = { 0x00 };
	for (int i = 0; i < 4; i++)
		gameID[i] = command[i + 2];

	std::cout << "Packet size: " << (int)packetSize << std::endl;
	std::cout << "Packet type: " << (int)packetType << std::endl;
	std::cout << "Game ID: ";
	for (int i = 0; i < 4; i++)
		std::cout << (int)gameID[i];
	std::cout << "" << std::endl;

}

int main()
{
	Player player;
	Game game;

	Log log;

	log.SetLogLevel("verbose");

	/*log.Trace("This is a Trace message");
	log.Warning("This is a Warning message");
	log.Error("This is an Error message");
	log.Fatal("This is a Fatal message");*/

	log.Trace("STARTING SERVER...");

	std::cout << "Server is starting..." << std::endl;

	// Link Ws2_32.lib.
#pragma comment (lib, "Ws2_32.lib")

	const char* PORT = "1234";
	const int DEFAULT_BUFLEN = 512;

	WSADATA wsaData;
	int iResult;

	SOCKET ListenSocket = INVALID_SOCKET;
	SOCKET ClientSocket = INVALID_SOCKET;

	struct addrinfo *result = NULL;
	struct addrinfo hints;

	int iSendResult;
	char recvbuf[DEFAULT_BUFLEN] = { 0x00 };
	int recvbuflen = DEFAULT_BUFLEN;

	// Initialize Winsock.
	iResult = WSAStartup(MAKEWORD(2, 2), &wsaData);
	if (iResult != 0)
	{
		log.Error("INITIALIZE WINSOCK FAILED: " + iResult);
		return 1;
	}
	else
		log.Trace("INITIALIZE WINSOCK OK");

	ZeroMemory(&hints, sizeof(hints));
	hints.ai_family = AF_INET;
	hints.ai_socktype = SOCK_STREAM;
	hints.ai_protocol = IPPROTO_TCP;
	hints.ai_flags = AI_PASSIVE;

	// Resolve the server address and port (host).
	iResult = getaddrinfo(NULL, PORT, &hints, &result);
	if (iResult != 0)
	{
		log.Error("RESOLVE HOST FAILED: " + iResult);
	}
	else
		log.Trace("RESOLVE HOST OK");

	// Create a SOCKET for connecting to server.
	ListenSocket = socket(result->ai_family, result->ai_socktype, result->ai_protocol);
	if (ListenSocket == INVALID_SOCKET)
	{
		std::stringstream logString;
		logString << "CREATE SOCKET FAILED: " << WSAGetLastError();
		log.Error(logString.str().c_str());
		freeaddrinfo(result);
		WSACleanup();
		return 1;
	}
	else
		log.Trace("CREATE SOCKET OK");

	// Setup the TCP listening socket.
	iResult = bind(ListenSocket, result->ai_addr, (int)result->ai_addrlen);
	if (iResult == SOCKET_ERROR)
	{
		std::stringstream logString;
		logString << "BIND SOCKET FAILED: " << WSAGetLastError();
		log.Error(logString.str().c_str());

		freeaddrinfo(result);
		closesocket(ListenSocket);
		WSACleanup();
		return 1;
	}
	else
		log.Trace("BIND SOCKET OK");

	freeaddrinfo(result);

	iResult = listen(ListenSocket, SOMAXCONN);
	if (iResult == SOCKET_ERROR)
	{
		std::stringstream logString;
		logString << "SET LISTENER FAILED: " << WSAGetLastError();
		log.Error(logString.str().c_str());
		closesocket(ListenSocket);
		WSACleanup();
		return 1;
	}
	else
		log.Trace("SET LISTENER OK");

	// Accept the cliet socket.
	ClientSocket = accept(ListenSocket, NULL, NULL);
	if (ClientSocket == INVALID_SOCKET)
	{
		std::stringstream logString;
		logString << "ACCEPT CLIENT FAILED: " << WSAGetLastError();
		log.Error(logString.str().c_str());
		closesocket(ListenSocket);
		WSACleanup();
		return 1;
	}
	else
		log.Trace("ACCEPT CLIENT OK");

	// No longer need server socket, yeet it.
	closesocket(ListenSocket);

	log.Trace("SERVER STARTUP COMPLETE");

	std::cout << "Startup successful" << std::endl;

	// Receive until the peer shuts down the connection.
	do {
		iResult = recv(ClientSocket, recvbuf, recvbuflen, 0);
		if (iResult > 0)
		{
			std::cout << "<client> " << recvbuf << std::endl;

			// Echo result back to sender.
			/*std::cout << "ECHO ";
			iSendResult = send(ClientSocket, recvbuf, iResult, 0);
			if (iSendResult == SOCKET_ERROR)
			{
				std::cout << "ERROR: " << WSAGetLastError() << std::endl;
				WSACleanup();
				return 1;
			}
			std::cout << "OK" << std::endl;*/

			Parse(recvbuf);
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
	log.Trace("SHUTDOWN COMMAND RECEIVED");
	std::cout << "SHUTTING DOWN" << std::endl;
	iResult = shutdown(ClientSocket, SD_SEND);
	if (iResult == SOCKET_ERROR)
	{
		log.Error("SHUTDOWN FAILED");
		closesocket(ClientSocket);
		WSACleanup();
		return 1;
	}

	// Cleanup.
	closesocket(ClientSocket);
	WSACleanup();

	log.Trace("SHUTDOWN COMPLETE");
	return 0;
}