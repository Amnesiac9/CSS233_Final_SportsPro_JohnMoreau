// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function updatePhoneNumber(id) {

    let formatPhoneNumber = (str) => {
        //Filter only numbers from the input
        let cleaned = ('' + str).replace(/\D/g, '');

        //Check if the input is of correct
        let match = cleaned.match(/^(1|)?(\d{3})(\d{3})(\d{4})$/);

        if (match) {
            //Remove the matched extension code
            //Change this to format for any country code.
            let intlCode = (match[1] ? '+1 ' : '')
            return [intlCode, '(', match[2], ') ', match[3], '-', match[4]].join('')
        }

        return null;
    }

    let phoneNumberInput = document.getElementById(id);

    let formattedPhoneNumber = formatPhoneNumber(phoneNumberInput.value);

    if (formattedPhoneNumber) {
        phoneNumberInput.value = formattedPhoneNumber
    }
    else {
        console.error(`Invalid Phone Number: ${phoneNumberInput.value}`);
    }


}


