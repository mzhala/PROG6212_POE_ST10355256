function validateForm() {
    // Get the values of the textboxes
    var lecturerNumber = document.getElementById('<%= TextBox1.ClientID %>').value;
    var lecturerName = document.getElementById('<%= TextBox2.ClientID %>').value;
    var lecturerSurname = document.getElementById('<%= TextBox3.ClientID %>').value;
    var month = document.getElementById('<%= TextBox4.ClientID %>').value;
    var year = document.getElementById('<%= TextBox5.ClientID %>').value;
    var programCode = document.getElementById('<%= TextBox6.ClientID %>').value;
    var module = document.getElementById('<%= TextBox7.ClientID %>').value;
    var rate = document.getElementById('<%= TextBox8.ClientID %>').value;
    var hours = document.getElementById('<%= TextBox9.ClientID %>').value;
    var supportDocument = document.getElementById('<%= TextBox10.ClientID %>').value;

    // Check for empty fields
    if (!lecturerNumber) {
        alert("Please fill in data for Lecturer Number.");
        return false;
    }
    if (!lecturerName) {
        alert("Please fill in data for Lecturer Name.");
        return false;
    }
    if (!lecturerSurname) {
        alert("Please fill in data for Lecturer Surname.");
        return false;
    }
    if (!month) {
        alert("Please fill in data for Month.");
        return false;
    }
    if (!year) {
        alert("Please fill in data for Year.");
        return false;
    }
    if (!programCode) {
        alert("Please fill in data for Program Code.");
        return false;
    }
    if (!module) {
        alert("Please fill in data for Module.");
        return false;
    }
    if (!rate) {
        alert("Please fill in data for Rate/hr.");
        return false;
    }
    if (!hours) {
        alert("Please fill in data for Hours.");
        return false;
    }
    if (!supportDocument) {
        alert("Please fill in data for Support Document.");
        return false;
    }

    // If all fields are filled, return true to allow form submission
    return true;
}
