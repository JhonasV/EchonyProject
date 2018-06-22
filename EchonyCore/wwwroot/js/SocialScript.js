$(document).ready(() => {

    const handleLoader = (action) => {
        if (action === 'show') {
            $("#load").removeAttr("style");
        } else if (action === 'hide') {
            $("#load").attr("style", "display:none;");
        }
       
    }

    $("#publicar-post").on("click", () => {
        let textArea = $("#text-estado");
        console.log(textArea.val());
        if (textArea.val() !== '') {
            handleLoader('show');
        } 
    })

    $("#AgregarPublicacion").ajaxForm((data) => {  
        if (data) {
            publicacionTextCleaner();
            CargadorPublicaciones();
        }
    })
/*
    $("#comment_form").ajaxForm((data) => {
        console.log(data);
    })
    */

    const publicacionTextCleaner = () => {
        let textArea = $("#text-estado");
        let placeholder = textArea.attr("placeholder");
        textArea.val("");
        textArea.attr("placeholder", placeholder);
        console.log(placeholder);   
    }

    const CargadorPublicaciones = () => {
        let userId = $("#userId").val();
        if (userId) {
            $("#publicaciones-wrapper").load(`/Perfil/Publicaciones/?userId=${userId}`);
           
        }
    }


   /* $(document).keypress(".input-comentario", (event)=> {
        if (event.keycode === 13) { // enter key has code 13 
            //some ajax code code here 
            //alert("Enter key pressed");
            consoloe.log(event.target.id);
        }
    });*/

    window.load = CargadorPublicaciones();



})