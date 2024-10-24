// formValidation.js

// Initialize an array to store the data
let dataArray = [];

// Function to validate form inputs
function validateForm() {
    // Get values from input fields using their IDs
    const lecturerNumber = document.getElementById('<%= TextBox1.ClientID %>').value;
    const lecturerName = document.getElementById('<%= TextBox2.ClientID %>').value;
    const lecturerSurname = document.getElementById('<%= TextBox3.ClientID %>').value;
    const month = document.getElementById('<%= TextBox4.ClientID %>').value;
    const year = document.getElementById('<%= TextBox5.ClientID %>').value;
    const programCode = document.getElementById('<%= TextBox6.ClientID %>').value;
    const module = document.getElementById('<%= TextBox7.ClientID %>').value;
    const rate = document.getElementById('<%= TextBox8.ClientID %>').value;
    const hours = document.getElementById('<%= TextBox9.ClientID %>').value;
    const supportDocument = document.getElementById('<%= TextBox10.ClientID %>').value;

    // Check for empty fields
    if (lecturerNumber === '') {
        alert("Please fill in data for 'Lecturer Number'");
        return false;
    }
    if (lecturerName === '') {
        alert("Please fill in data for 'Lecturer Name'");
        return false;
    }
    if (lecturerSurname === '') {
        alert("Please fill in data for 'Lecturer Surname'");
        return false;
    }
    if (month === '') {
        alert("Please fill in data for 'Month'");
        return false;
    }
    if (year === '') {
        alert("Please fill in data for 'Year'");
        return false;
    }
    if (programCode === '') {
        alert("Please fill in data for 'Program Code'");
        return false;
    }
    if (module === '') {
        alert("Please fill in data for 'Module'");
        return false;
    }
    if (rate === '') {
        alert("Please fill in data for 'Rate/hr'");
        return false;
    }
    if (hours === '') {
        alert("Please fill in data for 'Hours'");
        return false;
    }
    if (supportDocument === '') {
        alert("Please fill in data for 'Support Document'");
        return false;
    }

    // If all fields are filled, add data to the array
    dataArray.push({
        lecturerNumber: lecturerNumber,
        lecturerName: lecturerName,
        lecturerSurname: lecturerSurname,
        month: month,
        year: year,
        programCode: programCode,
        module: module,
        rate: rate,
        hours: hours,
        supportDocument: supportDocument
    });

    // Optionally, you can log the array to the console to see the entered data
    console.log(dataArray);

    return true; // Allow the form submission if needed
}
