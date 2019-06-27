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

    //$("#AgregarPublicacion").submit(() => {
    //    $('body').loadingModal({ text: 'Agregando publicación...', 'animation': 'wanderingCubes' });
    //})

    //$("#AgregarPublicacion").ajaxForm((data) => {  
    //    if (data) {
    //        publicacionTextCleaner();
    //        CargadorPublicaciones();
    //        //toastr.success('La publicación se ha agregado exitosamente!', 'Aviso');
    //        $('body').loadingModal('hide');
    //    }
    //})

    $("#form-fotos").submit(() => {
        $('body').loadingModal({ text: 'Actualizando foto de perfil...', 'animation': 'wanderingCubes' });
    })

    $("#form-fotos").ajaxForm((data) => {
        if (data) {
            let avatar = `/images/Fotos_Usuarios/${data}`;
            /*$("#foto-perfil").attr("src", avatar);
            $("#foto-modal").attr("src", avatar);
            $("#nav-foto").attr("src", avatar);*/
            $(".pic").attr("src", avatar);
            $('body').loadingModal('hide');
           // toastr.success('La foto se ha actualizado exitosamente!', 'Actualización foto de perfil');
            $("#foto-form-reset").click();

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

    //window.load = CargadorPublicaciones();



})