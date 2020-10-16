# Faux-TP

## An alternative file transfer protocol

Project for CS 346 @ HSU

Designed by James Pelligra, Vanja Venezia, Ryan Beck, Candance M., Bradley Arline, Fernando Crespo, Grayson Beckert, Riley Heffernan

---------------------

## Design

### Connections

Two port design akin to FTP; one port for control information and one port for data transfer.

Utilizes TCP connections for both control and data transfer for stability.

Ports are 3461 for control, 3462 for data.

### Message Format

Control port keywords: "SEND" and "RECV", indicating either a file upload or download, respectively. These will be utilized in the request header. "CONF" and "DENY" for noting whether a request is approved or denied. "DELE" for requesting a file deletion, and "OVER" to overwrite a file

Files will be sent with file name, file extension, and size of total datagram.

Encoding will always be UTF-8 for control keywords and file information.

#### Control Port Message:

##### Client Request:
| length of *.csv |
*.csv: | SEND or RECV or DELE or OVER | file name | file extension |
(UTF-8 strings)

| length  | type | description   |
|---------|------|---------------|
| 4 bytes | INT  | length of *.csv |
| variable| string| *.csv of file info|

##### Server Response:
| length of *.csv |
*.csv: | ERROR_CODE | datagram size (if successful) |

| length  | type | description   |
|---------|------|---------------|
| 4 bytes | INT  | length of *.csv |
| variable| string| *.csv of file info|

Error Codes:
0: NOERR - No Error
1: FAILW - Failure to Write
2: FAILR - Failure to Read
3: FILEE - File Exists
4: PERMS - Lack Permissions



#### Data Port Message:

Data port sends datagram size first, then raw data

### Data Formatting

Control messages and datagram size sent as *.csv, *.csv length sent as INT32.
(i.e.: 1234 "SEND,test,txt")

File extension should be sent without the "." but program should be able to detect and remove extra ".".

Files will be sent as raw byte data on data port.

---------------------

## General Features

### File operatons

Faux-TP supports sending and receiving files between a client and a server or peer-to-peer.
Faux-TP supports remote file deletion and updating through rewrites.

### Authentication

Faux-TP supports user authentication.

---------------------

## UI Specification