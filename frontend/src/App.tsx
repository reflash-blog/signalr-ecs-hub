import React, { useEffect, useState } from "react";
import Button from "./Button";
import { HttpTransportType, HubConnection, HubConnectionBuilder } from '@microsoft/signalr';

function App() {
  const [connection, setConnection] = useState<HubConnection | undefined>(undefined);
  const [count, setCount] = useState(0);

  useEffect(() => {
    const connect = async () => {
      console.log('connecting');
      const newConnection = new HubConnectionBuilder()
        .withUrl("https://localhost:7093/MessageHub", {
          transport: HttpTransportType.WebSockets
        })
        .withAutomaticReconnect()
        .build();

      await newConnection.start();
      await newConnection.send("JoinGroup", "user-id");
      setConnection(newConnection);
    }
    
    connect().catch(console.log);
  }, []);

  useEffect(() => {
    if (!connection) return;
    
    connection.on("inc", data => {
      setCount(count + 1);
    });

    connection.on("dec", data => {
      setCount(count - 1);
    });
  }, [connection, count, setCount])

  const incrementCount = async () => {
    if (connection)
      await connection.send("BroadcastMessage", "user-id", "inc", "a");
  };

  const decrementCount = async () => {
    if (connection)
      await connection.send("BroadcastMessage", "user-id", "dec", "b");
  };

  return (
    <div className="app">
      <p>Count: {count}</p>
      <div className="buttons">
        <Button title={"Decrement"} action={decrementCount} />
        <Button title={"Increment"} action={incrementCount} />
      </div>
    </div>
  );
}

export default App;