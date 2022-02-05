window.addEventListener('load', function () {
    var app = new Vue({
        el: '#app',
        data: {
            Employess: [],
            employeeid: "",
            name: "",
            pid: "",
            age: "",
            salary: "",
            pname: "",
            gender: "",
            isactive: "",
            Positions: [],
            submitted: false,
            email: null,
            reg: /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,24}))$/,
            errorEmail: {},
        },
        computed: {
            nameIsValid() {
                return !!this.name
            },
            pidIsValid() {
                return !!this.pid
            },
            ageIsValid() {
                return typeof this.age === 'number' && this.age > 18 && this.age < 120
            },
            salaryIsValid() {
                return !!this.salary
            },
            genderIsValid() {
                return !!this.gender
            },
            isactiveIsValid() {
                return !!this.isactive
            },
        },
        mounted: function () {
            this.GetEmployee();
        },
        methods: {
            // this Employee List Function
            GetEmployee() {
                const URL = "/Employees/GetEmployee";
                axios.get(URL).then(res => {
                    console.log(JSON.parse(res.data));
                    this.Employess = JSON.parse(res.data);
                })
            },
            // Clear form data
            CleanForm: function () {
                this.employeeid = null;
                this.name = null;
                this.pid = null;
                this.age = null;
                this.salary = null;
                this.gender = null;
                this.isactive = null;
            },
            // This get employee infor and update.
            EditInfo: function (event) {
                const URL2 = "/Employees/Edit?id=" + event;
                axios.get(URL2).then(res => {

                    var rec = JSON.parse(res.data);
                    this.employeeid = rec.EmployeeID;
                    this.name = rec.Name;
                    this.pid = rec.PID;
                    this.age = rec.Age;
                    this.email = rec.Email;
                    this.salary = rec.Salary;
                    this.gender = rec.Gender;
                    this.isactive = rec.IsActive;
                    //                    console.log(JSON.parse(res.data));

                    $('#EmployeeID').val(this.employeeid);
                    $('#Name').val(this.name);
                    $('#Age').val(this.age);
                    $('#Email').val(this.email);
                    $('#Salary').val(this.salary);
                    $('#Male').val(this.gender);
                    $('#Female').val(this.gender);
                    $('#IsActive').val(this.isactive);

                    const URL5 = "/Employees/GetPositionList";
                    axios.post(URL5).then(res => {
                        this.Positions = JSON.parse(res.data);
                        $('#PID').val(JSON.parse(res.data.PositionsID));
                    })
                    this.toggleModal();
                })
            },
            // Save dada model show
            SaveModelOpen: function () {
                this.CleanForm();
                const URL5 = "/Employees/GetPositionList";
                axios.post(URL5).then(res => {
                    $('#myModal').modal('show');
                    //                  console.log(JSON.parse(res.data));
                    this.Positions = JSON.parse(res.data);
                })
            },
            toggleModal: function (event) {
                $('#myModal').modal('show');
            },
            // Save and update both employee data singel modal working
            SaveBtn() {
                this.validateEmail();
                var formIsvalid = this.nameIsValid && this.ageIsValid && this.pidIsValid && this.salaryIsValid && this.genderIsValid && this.isactiveIsValid
                var vm = this;
                var employee = {
                    EmployeeID: vm.employeeid != null ? vm.employeeid : 0,
                    Name: vm.name,
                    PID: vm.pid,
                    Age: vm.age,
                    Email: vm.email,
                    Salary: vm.salary,
                    Gender: vm.gender,
                    IsActive: vm.isactive
                }

                if (formIsvalid) {
                    if (employee.EmployeeID != "" && employee.EmployeeID != 0) {
                        const URL4 = "/Employees/Edit";
                        axios.post(URL4, employee).then(res => {
                            $('#myModal').modal('hide');
                            alert(res.data);
                            //                        console.log(res.data);
                            this.GetEmployee();
                        })
                    } else {
                        const URL3 = "/Employees/Create";
                        axios.post(URL3, employee).then(res => {
                            $('#myModal').modal('hide');
                            alert(res.data);
                            //                      console.log(res.data);
                            this.GetEmployee();
                        })
                    }
                } else {
                    alert("Invalid employee form")
                }
            },
            // delete employee details
            DeleteInfo: function (event) {
                if (confirm('are you sure?'))
                    var URL2 = "/Employees/Delete?id=" + event;
                //var rec = res.data;
                axios.post(URL2).then(res => {
                    //                    console.log(rec.data);
                    this.GetEmployee();
                })
            },
            validateEmail() {
                if (this.email == null || this.email == '') {
                    $('#errorMsg').text("Please Enter Email");
                }
                else if (!this.reg.test(this.email)) {
                    $('#errorMsg').text("Please Enter Correct Email");
                }
                else if (!this.reg.test(this.email) == true) {

                    $('#errorMsg').style.display = "none";
                }
            }
        }
    });
});