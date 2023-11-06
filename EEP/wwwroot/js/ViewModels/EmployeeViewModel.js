ko.extenders.checkDate = function (target, option) {
    target.subscribe(function (newValue) {
        var spanElement = document.getElementById("DateOfJoiningValidator");
        if (new Date(newValue) < new Date(2023, 0, 1)) {
            console.log(newValue);
            spanElement.textContent = "Date must be greater than 01-01-2023";
        }
        else if (newValue == "") {
            spanElement.textContent = "Date is required";
        }
        else {
            spanElement.textContent = "";
        }
    });
    return target;
};
ko.extenders.checkEmail = function (target, option) {
    target.subscribe(function (newValue) {
        var spanElement = document.getElementById(option.id);
        var pattern = new RegExp("^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}$");
        if (pattern.test(newValue) == false || newValue=="") {
            spanElement.textContent = "Enter valid Email";
        }
        else {
            spanElement.textContent = "";
        }

       
    });
    return target;
};

ko.extenders.required = function (target, options) {
    target.subscribe(function (newValue) {
        var elementId = options.id; // Get the associated element's ID
        var spanElement = document.getElementById(elementId);
        if (newValue === "") {
            // Set its content to the custom error message
            spanElement.textContent = options.errorMessage;
        }
        else if (newValue.length>15) {
            spanElement.textContent ="Length should not exceed 15 characters"
        }
        else {
            spanElement.textContent = "";
        }
    });
    return target;
};

function EmployeeViewModel() {
    var self = this;
    self.FirstName = ko.observable("").extend({ required: { errorMessage: "First Name is required", id: "FirstNameValidator" } });
    self.LastName = ko.observable("").extend({ required: { errorMessage: "Last Name is required", id: "LastNameValidator" } });
    self.Email = ko.observable("").extend({ checkEmail: { errorMessage: "Email is required", id: "EmailValidator" } });
    self.JobTitle = ko.observable("").extend({ required: { errorMessage: "JobTitle is required", id: "JobTitleValidator" } });
    self.DateOfJoining = ko.observable("").extend({ checkDate: "Date Of Joining is required" });
    self.EmployeeStatus = ko.observable(false);
    self.data = ko.observableArray([]);
    self.isEmployeesActive = ko.observable(false);

    self.Submit = ko.computed(function () {
        return self.FirstName() !== "" && self.LastName() !== "" && self.JobTitle() !== "" && self.DateOfJoining() !== "";
    });;

    self.Create = function () {
        var employeeData = {
            FirstName: self.FirstName(),
            LastName: self.LastName(),
            Email:self.Email(),
            JobTitle: self.JobTitle(),
            DateOfJoining: self.DateOfJoining(),
            EmployeeStatus: self.EmployeeStatus()
            // Add other fields as needed.
        };
        $.ajax({
            url: "/employees/create", // Replace with your controller and action
            method: "POST",
            data: employeeData,
            success: function (response) {
                alert("Form submitted successfully!");
            },
            error: function (error) {
                alert("Form submission failed!");
            }
        });
    };
    self.load = function () {
            ;         // Initialize an empty array

        // Fetch data from the server using AJAX
        $.ajax({
            url: '/employees/ActiveEmployeesList', // Replace with your controller and action
            method: 'GET',
            success: function (data) {
                if (data.length != 0) {
                    self.isEmployeesActive(true);
                }
                self.data(data); // Populate the data array with the response
            },
            error: function (error) {
                console.error('Error fetching data:', error);
            }
        });
    }
    self.load();
    self.Edit = function () {
        var employeeData = {
            FirstName: self.FirstName(),
            LastName: self.LastName(),
            Email: self.Email(),
            JobTitle: self.JobTitle(),
            DateOfJoining: self.DateOfJoining(),
            EmployeeStatus: self.EmployeeStatus()
            // Add other fields as needed.
        };
        $.ajax({
            url: "/employees/edit", // Replace with your controller and action
            method: "POST",
            data: { "employeeData": employeeData, "id": document.getElementById("Id").value },
            success: function (response) {
                alert("Form submitted successfully!");
            },
            error: function (error) {
                alert("Form submission failed!");
            }
        });
    };

    self.Delete = function () {
        $.ajax({
            url: "/employees/DeleteConfirmed", // Replace with your controller and action
            method: "POST",
            data: { "id": document.getElementById("Id").value },
            success: function (response) {
                alert("Form submitted successfully!");
            },
            error: function (error) {
                alert("Form submission failed!");
            }
        });
    };
}

ko.applyBindings(new EmployeeViewModel());