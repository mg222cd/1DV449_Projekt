window.addEventListener("load", checkConnection, false);

//first step. check if client is connected
function checkConnection() {
    var onlineStatus = navigator.onLine;
    if (onlineStatus == true) {
        //
    }
    else {
        window.location.replace("http://localhost:52297/Offline.html")
    }
}

function storeData() {
        /*
        // Hämtar ut innehållet i local storage variabeln 
        var storedData = localStorage.getItem("searches");
        var storedSearch = JSON.parse(storedData);

        // Om det finns något i variabeln...
        if (storedSearch != null) {

            // Om sökningen redan finns i arrayen tas den gamla bort
            if ($.inArray(search, storedSearch) !== -1) {
                storedSearch.splice($.inArray(search, storedSearch), 1);
            }
            // Tar bort den äldsta sökningen om det finns fler än 10 element
            if (storedSearch.length > 10) {
                storedSearch.shift();
            }

            // Lägger till sökningen i strängen
            storedSearch.push(search);
            localStorage['searches'] = JSON.stringify(storedSearch);
        }
            // ...annars skapas en helt ny array som prognoserna läggs till i.
        else {
            var newArray = [];
            newArray.push(search);
            localStorage['searches'] = JSON.stringify(newArray);
        }
        */
}

