<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="pokedex_web.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Hola!</h1>
    <p>Legaste al Pokedex Web, tu lugar Pokemon...</p>

    <div class="row row-cols-1 row-cols-md-3 g-4">
        
        <%foreach (var item in ListaPokemon)
            { 
        %>

        <div class="col">
            <div class="card">
                <img src="<%:item.UrlImagen %>" class="card-img-top" alt="...">
                <div class="card-body">
                    <h5 class="card-title"><%:item.Nombre %></h5>
                    <p class="card-text"><%:item.Descripcion %></p>
                    <a href="DetallePokemon.aspx?id=<%:item.Id %>">Ver Detalle</a>
                </div>
            </div>
        </div>

           <% } %>
        

    </div>



</asp:Content>
