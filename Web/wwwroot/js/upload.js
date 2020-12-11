"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/uploadHub").build();

//Disable send button until connection is established
//document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (message) {
    //var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    //var encodedMsg = user + " says " + msg;
    //document.getElementById("msg").innerText = message;
    $('#mensagem').text(message);
    
    //var li = document.createElement("li");
    //li.textContent = encodedMsg;
    //document.getElementById("messagesList").appendChild(li);
});

connection.start().then(function () {
    //document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

//document.getElementById("sendButton").addEventListener("click", function (event) {
//    //var user = document.getElementById("userInput").value;
//    //var message = document.getElementById("messageInput").value;
//    connection.invoke("SendMessage", "Daniel", "Teste").catch(function (err) {
        
//        return console.error(err.toString());
//    });
//    event.preventDefault();
//});