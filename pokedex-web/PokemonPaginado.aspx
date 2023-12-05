<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="PokemonPaginado.aspx.cs" Inherits="pokedex_web.PokemonPaginado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%
        var activarBotonAnterior = PaginacionRespuesta.Pagina > 1;
        var activarBotonSiguente = PaginacionRespuesta.Pagina < PaginacionRespuesta.CantidadTotalPaginas;
    %>
    <h1>Hola!</h1>
    <p>Legaste al Pokedex Web, tu lugar Pokemon...</p>

    <div class="row row-cols-1 row-cols-md-3 g-4 mb-3">

        <%foreach (var item in PaginacionRespuesta.Elementos)
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

        <% }%>
    </div>

    <div class="row">
        <div class="co-3">
            <div class="my-1">
                <label class="form-label">Cantidad</label>
                <asp:DropDownList ID="ddlPaginas" CssClass="form-select" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlPaginas_SelectedIndexChanged">
                    <asp:ListItem Text="3" />
                    <asp:ListItem Text="5" />
                    <asp:ListItem Text="10" />
                    <asp:ListItem Text="25" />

                </asp:DropDownList>
            </div>
        </div>
    </div>

    <nav>
        <ul class="pagination">

            <%--Boton Anterior--%>
            <li class="page-item <%:(activarBotonAnterior ? null : "disabled") %>">
                <%if (activarBotonAnterior)
                    {%>

                <a class="page-link"
                    href="PokemonPaginado.aspx?pagina=<%:PaginacionRespuesta.Pagina - 1 %>&recordsPorPagina=<%:PaginacionRespuesta.RecordsPorPagina %>">Anterior
                </a>

                <%} %>
                <%
                    else
                    {
                %>
                <span class="page-link">Anterior</span>
                <% }%>
                
            </li>

            <%--Creamos n botones por n pagina--%>
            <%for (int pagina = 1; pagina <= PaginacionRespuesta.CantidadTotalPaginas; pagina++)
                {

            %>
            <li class="page-item <%:(pagina == PaginacionRespuesta.Pagina ? "active" : null) %>">

                <a class="page-link" href="PokemonPaginado.aspx?pagina=<%:pagina %>&recordsPorPagina=<%:PaginacionRespuesta.RecordsPorPagina %>"><%:pagina %></a>
            </li>

            <%    } %>

            <%--Boton Siguente--%>
            <li class="page-item <%:(activarBotonSiguente ? null : "disabled") %>">
                <%if (activarBotonSiguente)
                    { %>
                <a class="page-link"
                    href="PokemonPaginado.aspx?pagina=<%:PaginacionRespuesta.Pagina + 1 %>&recordsPorPagina=<%:PaginacionRespuesta.RecordsPorPagina %>">Siguiente
                </a>
                <%} %>
                <%
                    else
                    {
                %>
                <span class="page-link">Siguiente</span>
                <% }%>
            </li>
        </ul>
    </nav>

</asp:Content>
