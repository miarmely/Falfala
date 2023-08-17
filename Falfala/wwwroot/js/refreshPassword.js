import { updateResultLabel } from "./miarTools.js";


$(function () {
  
    $("#btn_showPasswords").click(function () {
        let type = $("#inpt_newPassword1").attr("type")

        //#region show passwords
        if (type == "password") {
            $("#inpt_newPassword1").attr("type", "text");  // show
            $("#inpt_newPassword2").attr("type", "text");  // show
            $("#btn_showPasswords img").attr("src", "./pictures/notVisible.png")  // add png        
        }
        //#endregion

        //#region hide passwords
        else if(type == "text"){
            $("#inpt_newPassword1").attr("type", "password");  // show 
            $("#inpt_newPassword2").attr("type", "password");  // show 
            $("#btn_showPasswords img").attr("src", "./pictures/visible.png")  // add png        
        }
        //#endregion
    });


    $("form").submit(async function (e) {
        e.preventDefault();

        //#region get passwords
        let password1 = $("#inpt_newPassword1").val();
        let password2 = $("#inpt_newPassword2").val();
        //#endregion

        //#region control passwords
        if (password1 != password2) {
            // save button
            await $("#btn_save").attr("disabled", "");  // disable
            await $("#btn_save").toggleClass("btn_hover");  // remove class

            await updateResultLabel("#td_resultLabel", "#btn_save", "Şifreler Birbirine <b style= 'color:red'>Eşit Değil</b>", 3);
            return;
        }
        //#endregion

        //#region update password
        let data = {
            password: $("#inpt_newPassword1").val()
        }

        $.ajax({
            method: "Delete",
            url: "api/user",
            data: JSON.stringify(data),
            contentType: "application/json",
            success: async function () {
                updateResultLabel("#td_resultLabel", "#btn_save", "<b style= 'color:green'>Barışıyla</b> Kaydedildi", 3);
            },
            error: async function (response){
                updateResultLabel("#td_resultLabel", "#btn_save", `<b style= 'color:red' ${response.responseText}</b>`, 3);
            }
        });
        //#endregion
    });
});