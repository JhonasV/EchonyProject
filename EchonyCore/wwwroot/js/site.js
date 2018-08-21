// Write your JavaScript code.

@using (Html.BeginForm("Autorizacion", "Login", FormMethod.Post, new { @Class = "formulario", id = "login-form" }))
{
    <div class="form_header">
        <div class="form_titulo">
            <h2>Ingresa a Echony</h2>
        </div>
    </div>
    <div class="form_body">
        <div class="fila">

            <div class="columna">
                @Html.EditorFor(model => model.UsuarioSesion.Email, new { htmlAttributes = new { placeholder = "Correo Eléctronico", @Class = "input-login input-size-m", id = "input-email" } })

            </div>
        </div>

        <div class="fila">

            <h3 style="text-align:center;">@Html.ValidationMessageFor(model => model.UsuarioSesion.Email, "", new { style = "color:red;" })</h3>

        </div>

        <div class="fila">

            <div class="columna">
                @Html.EditorFor(model => model.UsuarioSesion.Clave, new { htmlAttributes = new { placeholder = "Contraseña", @Class = "input-login input-size-m", id = "input-password" } })


            </div>

        </div>

        <div class="fila">
            <h3 style="text-align:center;">@Html.ValidationMessageFor(model => model.UsuarioSesion.Clave, "", new { style = "color:red;" })</h3>

        </div>
    </div>

    <div class="form_footer">
        <div class="fila">

            <div class="boton">
                <button class="btn" type="submit">Acceder</button>

            </div>



            <div class="registro_ask"><p>¿Aún no tienes una cuenta? <span class="span_login"><a class="link-registro" href="@Url.Action("Registro")">Registrate</a></span></p></div>


        </div>
        <span id="login-message"></span>

        <h2 style="text-align:center;color:white;background-color:#F04723;
                        font-size:25px; ">
            @Html.DisplayFor(model => model.UsuarioSesion.Mensaje)
        </h2>
       

    </div>
}
