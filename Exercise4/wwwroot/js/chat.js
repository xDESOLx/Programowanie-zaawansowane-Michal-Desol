"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (id, user, message, sentAt) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you
    // should be aware of possible script injection concerns.
    li.textContent = `(${new Date(sentAt).toLocaleString()}) ${user} says `;
    var span = document.createElement("span");
    span.style.whiteSpace = "pre-wrap";
    span.textContent = message;
    li.appendChild(span);

    const tr = document.createElement("tr")
    tr.id = `received-${id}`;
    tr.className = "fw-bold";
    tr.style.cursor = "pointer";
    tr.innerHTML = `
        <td>${new Date(sentAt).toLocaleString()}</td>
        <td>${user}</td>
        <td><span style="white-space: pre-wrap" id="content-${id}">${message}</span></td>`;
    tr.onclick = async () => {
        await connection.invoke("ConfirmReceipt", id);
        tr.className = "";
        tr.style.cursor = "auto";
    }
    document.getElementById("receivedMessagesTableBody").appendChild(tr);
});

connection.on("ReceiptConfirmed", id => {
    const messageRow = document.getElementById(`sent-${id}`);
    if (messageRow) {
        messageRow.className = "text-success";
    }
})

connection.on("MessageEdited", (id, newContent) => {
    const content = document.getElementById(`content-${id}`);
    if (content) {
        content.textContent = newContent;
    }
})

connection.on("MessageDeleted", id => {    
    const content = document.getElementById(`content-${id}`);
    if (content) {
        content.textContent = "Wiadomość usunięta.";
        content.className = "fst-italic";
    }
})

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var recipient = document.getElementById("recipientInput").value;
    var content = document.getElementById("contentInput").value;
    connection.invoke("SendMessage", recipient, content)
        .then(function (message) {
            const tr = `<tr id="sent-${message.id}">
                <td>${new Date(message.sentAt).toLocaleString()}</td>
                <td>${message.recipient}</td>
                <td><span style="white-space: pre-wrap">${message.content}</span></td>
            </tr>`
            document.getElementById("sentMessagesTableBody").innerHTML += tr;
        })
        .catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});