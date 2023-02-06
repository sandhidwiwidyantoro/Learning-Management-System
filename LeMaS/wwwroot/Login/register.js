$('#btnSubmit').click(function (e) {
    e.preventDefault();
    if ($('#formValidation').valid() == true) {
        register();
    }
    else {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Something went wrong!',
        })
    }
});

// ===== UNTUK VALIDASI INSERT DATA=====
$(function () {
    $("#formValidation").validate({
        rules: {
            nik: {
                required: true,
                minlength: 5,
                maxlength: 5
            },
            first_name: {
                required: true
            },
            last_name: {
                required: true
            },
            Email: {
                required: true,
                email: true
            },
            password: {
                required: true
            },
            birthdate: {
                required: true
            },
            gender: {
                required: true
            },
            tokenkelas: {
                required: true,
                minlength: 7,
                maxlength: 7
            }
        },
        messages: {
            nik: {
                required: "<p style='font-size: 12px; color: red; margin-bottom:-50px;'>*Please enter your nik</p>",
                minlength: "<p style='font-size: 12px; color: red; margin-bottom:-50px;'>*Nik should be at least 5 number</p>",
                maxlength: "<p style='font-size: 12px; color: red; margin-bottom:-50px;'>*Nik can't be longer than 5 number</p>"
            },
            first_name: {
                required: "<p style='font-size: 13px; color: red; margin-bottom:-50px;'>*Please enter your first_name</p>"
            },
            last_name: {
                required: "<p style='font-size: 13px; color: red; margin-bottom:-50px;'>*Please enter your last_name</p>"
            },
            Email: {
                required: "<p style='font-size: 13px; color: red; margin-bottom:-50px;'>*Please enter your email</p>",
                email: "<p style='font-size: 12px; color: red; margin-bottom:-50px;'>*The email should be in the format: abc@domain.tld</p>"
            },
            password: {
                required: "<p style='font-size: 13px; color: red; margin-bottom:-50px;'>*Please enter your password</p>"
            },
            birthdate: {
                required: "<p style='font-size: 13px; color: red; margin-bottom:-50px;'>*Please choose your birthday</p>"
            },
            gender: {
                required: "<p style='font-size: 13px; color: red; margin-bottom:-50px;'>*Please choose your gender</p>"
            },
            tokenkelas: {
                required: "<p style='font-size: 13px; color: red; margin-bottom:-50px;'>*Please enter your tokenkelas</p>",
                minlength: "<p style='font-size: 12px; color: red; margin-bottom:-50px;'>*Token should be at least 7 number</p>",
                maxlength: "<p style='font-size: 12px; color: red; margin-bottom:-50px;'>*Token can't be longer than 7 number</p>"
            }
        }
    });
});


function register() {
    var obj = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
    //ini ngambil value dari tiap inputan di form nya
    obj.nik = parseInt($("#NIK").val());
    obj.firstName = $("#FirstName").val();
    obj.lastName = $("#LastName").val();
    obj.email = $("#InputEmail").val();
    obj.password = $("#InputPassword").val();
    obj.birthDate = $("#birthDate").val();
    obj.gender = parseInt($("#gender").val());
    obj.tokenKelas = parseInt($("#tokenKelas").val()),
        console.log(obj);
    //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
    $.ajax({
        url: "https://localhost:7230/api/Employee/RegistrasiPeserta",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(obj)//jika terkena 415 unsupported media type (tambahkan headertype Json & JSON.Stringify();)
    }).done((result) => {
        //buat alert pemberitahuan jika success
        Swal.fire({
            text: 'Anda Berhasil Registrasi',
            icon: 'success',
            timer: 2000,
            timerProgressBar: true
        }).then(function () {
            window.location = "https://localhost:7295/Login/Index";
        });
    }).fail((error) => {
        //alert pemberitahuan jika gagal
        Swal.fire({
            text: 'Anda Gagal Registrasi',
            icon: 'error',
            timer: 2000,
            timerProgressBar: true
        });
    })
}