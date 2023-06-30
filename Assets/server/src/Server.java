import java.io.*;
import java.net.*;
import java.util.Formatter;
import java.util.Scanner;

public class Server {
    Socket socket;
    ServerSocket serverSocket;


    Server(int port)
    {
        try {
            serverSocket = new ServerSocket(port);
        }catch (Exception e)
        {

        }
    }
    public void SignIn() {
        try {
            InputStream is = socket.getInputStream();
            OutputStream os = socket.getOutputStream();
            PrintWriter writer = new PrintWriter(os);
            // Receiving
            byte[] lenBytes = new byte[4];
            is.read(lenBytes, 0, 4);
            int len = (((lenBytes[3] & 0xff) << 24) | ((lenBytes[2] & 0xff) << 16) |
                    ((lenBytes[1] & 0xff) << 8) | (lenBytes[0] & 0xff));
            byte[] receivedBytes = new byte[len];
            is.read(receivedBytes, 0, len);
            String name = new String(receivedBytes, 0, len);

            byte[] lenPass = new byte[4];
            is.read(lenPass, 0, 4);
            int Len = (((lenPass[3] & 0xff) << 24) | ((lenPass[2] & 0xff) << 16) |
                    ((lenPass[1] & 0xff) << 8) | (lenPass[0] & 0xff));
            byte[] receivedPass = new byte[Len];
            is.read(receivedPass, 0, Len);
            String password = new String(receivedPass, 0, Len);
            File file = new File("User.txt");
            if(!file.exists())
            {
                Writer fileWriter = new FileWriter("User.txt");
                String put = name + " " + password + " " + "0\n";
                fileWriter.write(put);
                fileWriter.flush();
                fileWriter.close();
                String toSend = "welcome to the game " + name;
                byte[] toSendBytes = toSend.getBytes();
                int toSendLen = toSendBytes.length;
                byte[] toSendLenBytes = new byte[4];
                toSendLenBytes[0] = (byte)(toSendLen & 0xff);
                toSendLenBytes[1] = (byte)((toSendLen >> 8) & 0xff);
                toSendLenBytes[2] = (byte)((toSendLen >> 16) & 0xff);
                toSendLenBytes[3] = (byte)((toSendLen >> 24) & 0xff);
                os.write(toSendLenBytes);
                os.write(toSendBytes);
            }
            else {
                while (true) {
                    Scanner scanner = new Scanner(file);
                    boolean flag = true;
                    while (scanner.hasNextLine()) {
                        String line = scanner.nextLine();
                        String[] splited = line.split(" ");
                        if (splited[0].equals(name)) {
                            String toSend = "this username have already exists... ";
                            byte[] toSendBytes = toSend.getBytes();
                            int toSendLen = toSendBytes.length;
                            byte[] toSendLenBytes = new byte[4];
                            toSendLenBytes[0] = (byte) (toSendLen & 0xff);
                            toSendLenBytes[1] = (byte) ((toSendLen >> 8) & 0xff);
                            toSendLenBytes[2] = (byte) ((toSendLen >> 16) & 0xff);
                            toSendLenBytes[3] = (byte) ((toSendLen >> 24) & 0xff);
                            os.write(toSendLenBytes);
                            os.write(toSendBytes);
                            flag = false;
                            break;
                        }
                    }
                    if(!flag)
                    {
                        break;
                    }
                    if (flag) {
                        FileWriter fileWriter = new FileWriter("User.txt", true);
                        String put = name + " " + password + " " + "0\n";
                        fileWriter.write(put);
                        fileWriter.flush();
                        fileWriter.close();
                        String toSend = "welcome to the game " + name;
                        byte[] toSendBytes = toSend.getBytes();
                        int toSendLen = toSendBytes.length;
                        byte[] toSendLenBytes = new byte[4];
                        toSendLenBytes[0] = (byte) (toSendLen & 0xff);
                        toSendLenBytes[1] = (byte) ((toSendLen >> 8) & 0xff);
                        toSendLenBytes[2] = (byte) ((toSendLen >> 16) & 0xff);
                        toSendLenBytes[3] = (byte) ((toSendLen >> 24) & 0xff);
                        os.write(toSendLenBytes);
                        os.write(toSendBytes);
                    }
                    writer.close();
                }
            }
        }catch (Exception e)
        {

        }

    }

    public void LogIn()
    {
        try {
            InputStream is = socket.getInputStream();
            OutputStream os = socket.getOutputStream();
            PrintWriter writer = new PrintWriter(os);
            // Receiving
            byte[] lenBytes = new byte[4];
            is.read(lenBytes, 0, 4);
            int len = (((lenBytes[3] & 0xff) << 24) | ((lenBytes[2] & 0xff) << 16) |
                    ((lenBytes[1] & 0xff) << 8) | (lenBytes[0] & 0xff));
            byte[] receivedBytes = new byte[len];
            is.read(receivedBytes, 0, len);
            String name = new String(receivedBytes, 0, len);

            byte[] lenPass = new byte[4];
            is.read(lenPass, 0, 4);
            int Len = (((lenPass[3] & 0xff) << 24) | ((lenPass[2] & 0xff) << 16) |
                    ((lenPass[1] & 0xff) << 8) | (lenPass[0] & 0xff));
            byte[] receivedPass = new byte[Len];
            is.read(receivedPass, 0, Len);
            String password = new String(receivedPass, 0, Len);
            File file = new File("User.txt");
            if(!file.exists())
            {
                String toSend = "there is no account yet. please sign up first...";
                byte[] toSendBytes = toSend.getBytes();
                int toSendLen = toSendBytes.length;
                byte[] toSendLenBytes = new byte[4];
                toSendLenBytes[0] = (byte) (toSendLen & 0xff);
                toSendLenBytes[1] = (byte) ((toSendLen >> 8) & 0xff);
                toSendLenBytes[2] = (byte) ((toSendLen >> 16) & 0xff);
                toSendLenBytes[3] = (byte) ((toSendLen >> 24) & 0xff);
                os.write(toSendLenBytes);
                os.write(toSendBytes);
            }
            else
            {
                Scanner scanner = new Scanner(file);
                boolean flag = false;
                while(scanner.hasNextLine())
                {
                    String line = scanner.nextLine();
                    String[] splited = line.split(" ");
                    if(splited[0].equals(name) && splited[1].equals(password))
                    {
                        String toSend = "you loged in successfully...";
                        byte[] toSendBytes = toSend.getBytes();
                        int toSendLen = toSendBytes.length;
                        byte[] toSendLenBytes = new byte[4];
                        toSendLenBytes[0] = (byte) (toSendLen & 0xff);
                        toSendLenBytes[1] = (byte) ((toSendLen >> 8) & 0xff);
                        toSendLenBytes[2] = (byte) ((toSendLen >> 16) & 0xff);
                        toSendLenBytes[3] = (byte) ((toSendLen >> 24) & 0xff);
                        os.write(toSendLenBytes);
                        os.write(toSendBytes);
                        flag = true;
                        scanner.close();
                    }
                }
                if (!flag)
                {
                    String toSend = "username or password you entered is not correct...";
                    byte[] toSendBytes = toSend.getBytes();
                    int toSendLen = toSendBytes.length;
                    byte[] toSendLenBytes = new byte[4];
                    toSendLenBytes[0] = (byte) (toSendLen & 0xff);
                    toSendLenBytes[1] = (byte) ((toSendLen >> 8) & 0xff);
                    toSendLenBytes[2] = (byte) ((toSendLen >> 16) & 0xff);
                    toSendLenBytes[3] = (byte) ((toSendLen >> 24) & 0xff);
                    os.write(toSendLenBytes);
                    os.write(toSendBytes);
                    scanner.close();
                }
            }

        }catch (Exception e)
        {

        }

    }

    public void ChangePass()
    {
        try{
            InputStream is = socket.getInputStream();
            OutputStream os = socket.getOutputStream();
            // Receiving
            byte[] lenBytes = new byte[4];
            is.read(lenBytes, 0, 4);
            int len = (((lenBytes[3] & 0xff) << 24) | ((lenBytes[2] & 0xff) << 16) |
                    ((lenBytes[1] & 0xff) << 8) | (lenBytes[0] & 0xff));
            byte[] receivedBytes = new byte[len];
            is.read(receivedBytes, 0, len);
            String name = new String(receivedBytes, 0, len);

            byte[] lenPass = new byte[4];
            is.read(lenPass, 0, 4);
            int Len = (((lenPass[3] & 0xff) << 24) | ((lenPass[2] & 0xff) << 16) |
                    ((lenPass[1] & 0xff) << 8) | (lenPass[0] & 0xff));
            byte[] receivedPass = new byte[Len];
            is.read(receivedPass, 0, Len);
            String newPassword = new String(receivedPass, 0, Len);
            File file = new File("User.txt");
            if(!file.exists())
            {
                String toSend = "there is no account yet. please sign up first...";
                byte[] toSendBytes = toSend.getBytes();
                int toSendLen = toSendBytes.length;
                byte[] toSendLenBytes = new byte[4];
                toSendLenBytes[0] = (byte) (toSendLen & 0xff);
                toSendLenBytes[1] = (byte) ((toSendLen >> 8) & 0xff);
                toSendLenBytes[2] = (byte) ((toSendLen >> 16) & 0xff);
                toSendLenBytes[3] = (byte) ((toSendLen >> 24) & 0xff);
                os.write(toSendLenBytes);
                os.write(toSendBytes);
            }
            else
            {
                File newFile = new File("new_User.txt");
                Formatter formatter = new Formatter("new_User.txt");
                Scanner scanner = new Scanner(file);
                boolean flag = false;
                while(scanner.hasNextLine()) {
                    String line = scanner.nextLine();
                    String[] splited = line.split(" ");
                    if (name.equals(splited[0])) {
                        splited[1] = newPassword;
                        line = splited[0] + " " + splited[1] + " " + splited[2] + "\n";
                        formatter.format(line);
                        formatter.flush();
                        flag = true;
                    } else {
                        String temp = splited[0] + " " + splited[1] + " " + splited[2] + "\n";
                        formatter.format(temp);
                        formatter.flush();
                    }
                }
                    if(flag)
                    {
                        String toSend = "you changed your password successfully...";
                        byte[] toSendBytes = toSend.getBytes();
                        int toSendLen = toSendBytes.length;
                        byte[] toSendLenBytes = new byte[4];
                        toSendLenBytes[0] = (byte) (toSendLen & 0xff);
                        toSendLenBytes[1] = (byte) ((toSendLen >> 8) & 0xff);
                        toSendLenBytes[2] = (byte) ((toSendLen >> 16) & 0xff);
                        toSendLenBytes[3] = (byte) ((toSendLen >> 24) & 0xff);
                        os.write(toSendLenBytes);
                        os.write(toSendBytes);
                        scanner.close();
                        formatter.close();
                        file.delete();
                        newFile.renameTo(file);
                        is.close();
                        os.close();
                    }
                    else
                    {
                        String toSend = "there is no account with this username...";
                        byte[] toSendBytes = toSend.getBytes();
                        int toSendLen = toSendBytes.length;
                        byte[] toSendLenBytes = new byte[4];
                        toSendLenBytes[0] = (byte) (toSendLen & 0xff);
                        toSendLenBytes[1] = (byte) ((toSendLen >> 8) & 0xff);
                        toSendLenBytes[2] = (byte) ((toSendLen >> 16) & 0xff);
                        toSendLenBytes[3] = (byte) ((toSendLen >> 24) & 0xff);
                        os.write(toSendLenBytes);
                        os.write(toSendBytes);
                        scanner.close();
                        formatter.close();
                        file.delete();
                        newFile.renameTo(file);
                        is.close();
                        os.close();
                    }
                }

        }catch (Exception e)
        {

        }
    }

    void changeName()
    {
        try {
            InputStream is = socket.getInputStream();
            OutputStream os = socket.getOutputStream();
            // Receiving
            byte[] lenBytes = new byte[4];
            is.read(lenBytes, 0, 4);
            int len = (((lenBytes[3] & 0xff) << 24) | ((lenBytes[2] & 0xff) << 16) |
                    ((lenBytes[1] & 0xff) << 8) | (lenBytes[0] & 0xff));
            byte[] receivedBytes = new byte[len];
            is.read(receivedBytes, 0, len);
            String oldName = new String(receivedBytes, 0, len);

            byte[] lenPass = new byte[4];
            is.read(lenPass, 0, 4);
            int Len = (((lenPass[3] & 0xff) << 24) | ((lenPass[2] & 0xff) << 16) |
                    ((lenPass[1] & 0xff) << 8) | (lenPass[0] & 0xff));
            byte[] receivedPass = new byte[Len];
            is.read(receivedPass, 0, Len);
            String newName = new String(receivedPass, 0, Len);
            File file = new File("User.txt");
            Scanner scanner = new Scanner(file);
            boolean flag = true;
            while(scanner.hasNextLine())
            {
                String line = scanner.nextLine();
                String[] splited = line.split(" ");
                if(splited[0].equals(newName))
                {
                    flag = false;
                    break;
                }
            }
            if(!flag)
            {
                String toSend = "false";
                byte[] toSendBytes = toSend.getBytes();
                int toSendLen = toSendBytes.length;
                byte[] toSendLenBytes = new byte[4];
                toSendLenBytes[0] = (byte) (toSendLen & 0xff);
                toSendLenBytes[1] = (byte) ((toSendLen >> 8) & 0xff);
                toSendLenBytes[2] = (byte) ((toSendLen >> 16) & 0xff);
                toSendLenBytes[3] = (byte) ((toSendLen >> 24) & 0xff);
                os.write(toSendLenBytes);
                os.write(toSendBytes);
                scanner.close();
                is.close();
                os.close();
            }
            else
            {
                File newFile = new File("new_User.txt");
                Formatter formatter = new Formatter(newFile);
                scanner.close();
                scanner = new Scanner(file);
                boolean flag1 = false;
                while (scanner.hasNextLine())
                {
                    String line = scanner.nextLine();
                    String[] splited = line.split(" ");
                    if(splited[0].equals(oldName))
                    {
                        splited[0] = newName;
                        flag1 = true;
                    }
                    line = splited[0] + " " + splited[1] + " " + splited[2] + "\n";
                    formatter.format(line);
                }
                if(flag1)
                {
                    String toSend = "true";
                    byte[] toSendBytes = toSend.getBytes();
                    int toSendLen = toSendBytes.length;
                    byte[] toSendLenBytes = new byte[4];
                    toSendLenBytes[0] = (byte) (toSendLen & 0xff);
                    toSendLenBytes[1] = (byte) ((toSendLen >> 8) & 0xff);
                    toSendLenBytes[2] = (byte) ((toSendLen >> 16) & 0xff);
                    toSendLenBytes[3] = (byte) ((toSendLen >> 24) & 0xff);
                    os.write(toSendLenBytes);
                    os.write(toSendBytes);
                    scanner.close();
                    formatter.close();
                    file.delete();
                    newFile.renameTo(file);
                    is.close();
                    os.close();
                }
                else
                {
                    String toSend = "non";
                    byte[] toSendBytes = toSend.getBytes();
                    int toSendLen = toSendBytes.length;
                    byte[] toSendLenBytes = new byte[4];
                    toSendLenBytes[0] = (byte) (toSendLen & 0xff);
                    toSendLenBytes[1] = (byte) ((toSendLen >> 8) & 0xff);
                    toSendLenBytes[2] = (byte) ((toSendLen >> 16) & 0xff);
                    toSendLenBytes[3] = (byte) ((toSendLen >> 24) & 0xff);
                    os.write(toSendLenBytes);
                    os.write(toSendBytes);
                    scanner.close();
                    formatter.close();
                    file.delete();
                    newFile.renameTo(file);
                    is.close();
                    os.close();
                }
            }
        }catch (Exception e)
        {

        }
    }

    void leaderBoard()
    {
        try {
            InputStream is = socket.getInputStream();
            OutputStream os = socket.getOutputStream();
            File file = new File("User.txt");
            Scanner scanner = new Scanner(file);
            while(scanner.hasNextLine())
            {
                String line = scanner.nextLine();
                System.out.println(line);
                String[] splited = line.split(" ");

                String toSend = splited[0];
                byte[] toSendBytes = toSend.getBytes();
                int toSendLen = toSendBytes.length;
                byte[] toSendLenBytes = new byte[4];
                toSendLenBytes[0] = (byte) (toSendLen & 0xff);
                toSendLenBytes[1] = (byte) ((toSendLen >> 8) & 0xff);
                toSendLenBytes[2] = (byte) ((toSendLen >> 16) & 0xff);
                toSendLenBytes[3] = (byte) ((toSendLen >> 24) & 0xff);
                os.write(toSendLenBytes);
                os.write(toSendBytes);

                String toSend1 = splited[2];
                byte[] toSendBytes1 = toSend1.getBytes();
                int toSendLen1 = toSendBytes1.length;
                byte[] toSendLenBytes1 = new byte[4];
                toSendLenBytes1[0] = (byte) (toSendLen1 & 0xff);
                toSendLenBytes1[1] = (byte) ((toSendLen1 >> 8) & 0xff);
                toSendLenBytes1[2] = (byte) ((toSendLen1 >> 16) & 0xff);
                toSendLenBytes1[3] = (byte) ((toSendLen1 >> 24) & 0xff);
                os.write(toSendLenBytes1);
                os.write(toSendBytes1);
            }
            String toSend1 = "end";
            byte[] toSendBytes1 = toSend1.getBytes();
            int toSendLen1 = toSendBytes1.length;
            byte[] toSendLenBytes1 = new byte[4];
            toSendLenBytes1[0] = (byte) (toSendLen1 & 0xff);
            toSendLenBytes1[1] = (byte) ((toSendLen1 >> 8) & 0xff);
            toSendLenBytes1[2] = (byte) ((toSendLen1 >> 16) & 0xff);
            toSendLenBytes1[3] = (byte) ((toSendLen1 >> 24) & 0xff);
            os.write(toSendLenBytes1);
            os.write(toSendBytes1);
             scanner.close();
        }catch (Exception e)
        {

        }
    }







    public static void main(String[] args) throws Exception{
            Server server = new Server(1234);
            while(true) {
                server.socket = server.serverSocket.accept();
                InputStream inputStream = server.socket.getInputStream();
                byte[] select = new byte[1];
                inputStream.read(select);
                String str = new String(select);
                switch (str) {
                    case "1":
                        server.SignIn();
                        break;
                    case "2":
                        server.LogIn();
                        break;
                    case "3":
                        server.ChangePass();
                        break;
                    case "4":
                        server.changeName();
                        break;
                    case "5":
                        server.leaderBoard();
                        break;
                    case "6":
                }
            }
    }
}
