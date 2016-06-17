<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="MonPainel" Codebehind="MonPainel.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<uc:NavMon ID="ucNavMon" runat="server" /><br />

  <asp:UpdatePanel ID="updatePanel" UpdateMode="always" runat="server">
  <ContentTemplate>
    <asp:Label ID="lblMsg" Visible="false" runat="server" Text="" EnableViewState="false"></asp:Label>
 <div class="bucket_container" style="background:none" runat="server" id="divFiltro">
 <span class="button"><asp:Button ID="btnShow" Text="Exibir Filtros" runat="server" /></span>
 </div>
<div class="heading_container"><div class="heading_right_top"></div>
<h2 id="H7">Resumo Executivo</h2></div>
<div class="bucket_container"><div class="bucket_top"><span></span></div><div class="bucket_content"><div class="clear"></div> 


  <ajaxToolKit:ModalPopupExtender ID="mdlPopup" 
   runat="server" TargetControlID="btnShow" 
   PopupControlID="pnlPopup" BackgroundCssClass="modalBackground" />
 
  <asp:Panel ID="pnlPopup" runat="server" CssClass="confirm-dialog" style="display:none;">
       <div class="inner">
        <h2>Filtros - Resumo Executivo</h2>
         <div class="base">
         <div class="table" >
         <asp:Label ID="lblMsgPopUp" Visible="false" runat="server" Text="" EnableViewState="false"></asp:Label>
           <table>
           <tr>
            <td>
            <h5 style="margin:0; padding-bottom:0">Projeto</h5>
                <asp:DropDownList Font-Size="Small" Width="500px" ID="ddlt03_cd_projeto" runat="server">
                </asp:DropDownList>
            </td>
           </tr>           
           <tr>
            <td>
            <h5 style="margin:0; padding-bottom:0">Área de Resultado</h5>
                <asp:DropDownList Font-Size="Small" Width="500px" ID="ddlt26_cd_arearesultado" runat="server">
                </asp:DropDownList>
            </td>
           </tr>
           <tr>
            <td>
            <h5 style="margin:0; padding-bottom:0">Entidade Responsável</h5>
                <asp:DropDownList Font-Size="Small" Width="500px" ID="ddlt01_cd_entidade_resp" runat="server">
                </asp:DropDownList>
            </td>
           </tr>
           <tr>
            <td>
            <h5 style="margin:0; padding-bottom:0">Entidade Parceira</h5>
                <asp:DropDownList Font-Size="Small" Width="500px" ID="ddlt01_cd_entidade_parc" runat="server">
                </asp:DropDownList>
            </td>
           </tr>
           <tr>
            <td>
            <h5 style="margin:0; padding-bottom:0">Fase do Projeto</h5>
                <asp:DropDownList Font-Size="Small" Width="500px" ID="ddlt21_cd_fase" runat="server">
                </asp:DropDownList>
            </td>
           </tr> 
           <tr>
            <td> 
            <br />  
           </td>
           </tr>                  
           <tr>
               <td>          
               <span class="button"><asp:Button ID="btnFiltrar" OnClick="btnFiltrar_Click" 
               CausesValidation="false" runat="server" Text="Filtrar" Width="150px" /></span>
              </td>
           </tr>
           </table>
           </div>
         </div>
      </div>
</asp:Panel>
                    

<asp:Panel ID="PanelMon" runat="server">
<table cellspacing="6" style="width:100%" >
<tr style="vertical-align:top; background:#FFFFFF">
    <td style="width:50%" class="dashed">
        <div class="monTitPainel">Atualização dos Projetos</div>
        <div style="text-align:center">
        <asp:Panel ID="pnAtualizacao" runat="server">
        </asp:Panel>
        <asp:HyperLink ID="linkAtualiza"
        NavigateUrl="~/MonMediaDias.aspx" runat="server"></asp:HyperLink>
        </div>
        <br />
    </td>
    <td class="dashed">
        <div class="monTitPainel">Marcos Críticos</div>
        <div style="text-align:center"><br />
         <center>
            <asp:Panel ID="pnMarcosStatus" Width="60%" runat="server">
            </asp:Panel>
            <br />
                        <table style="width:80%;" cellpadding="4" class="tblist">
                        <tr>
                        <td style="width:65%"><b>
                        Status</b></td>
                        <td colspan="2"><b>%</b></td>
                        <td ><b>Qtd</b></td>
                        </tr>

                        <tr>
                        <td style="text-align:left">
                        <span style="background:url('images/B.gif') center center; padding: 0 15px 0 15px">&nbsp;</span>
                        <asp:HyperLink ID="linkConcluidos"  runat="server">
                         Concluídos
                        </asp:HyperLink>
                        </td>
                        <td  colspan="2">
                            <asp:Label ID="lblFatiaAzul" runat="server"></asp:Label>
                            </td><td >
                             <asp:Label ID="lblAzul" runat="server"></asp:Label></td>
                        </tr>

                         <tr>
                        <td  style="text-align:left">
                        <span style="background:url('images/G.gif') center center; padding: 0 15px 0 15px">&nbsp;</span>
                        <asp:HyperLink ID="linkPrazos" runat="server">
                        Dentro dos prazos previstos
                        </asp:HyperLink>
                        </td>
                        <td colspan="2">
                        <asp:Label ID="lblFatiaVerde" runat="server"></asp:Label>
                             </td><td>
                                 <asp:Label ID="lblVerde" runat="server"></asp:Label></td>
                        </tr>

                        <tr>
                        <td  style="text-align:left">
                        <span style="background:url('images/Y.gif') center center; padding: 0 15px 0 15px">&nbsp;</span>
                        <asp:HyperLink ID="linkComRestricoes"  runat="server">
                         Com restrições
                        </asp:HyperLink>
                        </td>
                        <td  colspan="2">
                        <asp:Label ID="lblFatiaAmarela"  runat="server"></asp:Label>
                            </td><td >
                                <asp:Label ID="lblAmarela" runat="server"></asp:Label></td>
                        </tr>

                        <tr>
                        <td  style="text-align:left">
                        <span style="background:url('images/R.gif') center center; padding: 0 15px 0 15px">&nbsp;</span>
                        <asp:HyperLink ID="linkAtraso" runat="server">    
                            Com atraso
                        </asp:HyperLink>    
                            </td>
                        <td colspan="2">
                        <asp:Label ID="lblFatiaVermelha" runat="server"></asp:Label>
                            </td><td>
                          <asp:Label ID="lblVermelha" runat="server"></asp:Label></td>
                        </tr>
                        </table>
          </center>            
        </div>
    </td>
</tr>
<tr style="vertical-align:top">
    <td class="dashed" >
        <div class="monTitPainel">Acompanhamento Físico - Financeiro</div>
        <div style="text-align:center;padding-top:10px;font-weight:bold;"> 
         <asp:Label ID="lblObs" runat="server" Text="Label"></asp:Label>
        </div>           
        <div style="text-align:center">            
            <asp:Panel ID="pnFisicoFinanceiro" runat="server">
            </asp:Panel>
            <asp:HyperLink ID="linkFisico"  NavigateUrl="~/MonFisicoGraf.aspx" Visible="false" runat="server">Acompanhamento Físico</asp:HyperLink> &nbsp;&nbsp;
            <asp:HyperLink ID="linkFinanceiro"  NavigateUrl="~/MonFinanceiroGraf.aspx" runat="server">Acompanhamento Financeiro </asp:HyperLink>
            <br />
        </div>
    </td>
    <td class="dashed">
        <div class="monTitPainel">Indicadores</div>
        <div style="text-align:center">
         <center>
        <br />
        <br />
            <table style="width:80%;text-align:left" cellpadding="4" class="tblist">
	            <tr>
	            <td>
	            <asp:HyperLink ID="linkParceiros" NavigateUrl="~/MonParceiros.aspx" runat="server">
	               - Quantidade média de parceiros	
	              </asp:HyperLink>
	              </td>
	            <td style="text-align:center">
	            	<asp:Label ID="lblimobilizacao" runat="server" Text="0,00"></asp:Label></td>
	            </tr>

	            <tr>
	            <td>
	            <asp:HyperLink ID="linkRealFisica"  NavigateUrl="~/MonFisicoInd.aspx" runat="server">
	               - Índice de realização física
	              </asp:HyperLink>
	              </td>
	            <td style="text-align:center">
	                <asp:Label ID="lblifisica" runat="server" Text="0,00"></asp:Label></td>
	            </tr>

	            <tr>
	            <td>
	            <asp:HyperLink ID="linkRealFinanceira"  NavigateUrl="~/MonFinanceiroInd.aspx" runat="server">
	               - Índice de realização financeira
	              </asp:HyperLink>
	              </td>
	            <td style="text-align:center">
	                <asp:Label ID="lblifinanceira" runat="server" Text="0,00"></asp:Label></td>
	            </tr>
	            <tr>
	            <td colspan="2" style="border:none">
	              </td>
                </tr>
	            <tr>
	            <td colspan="2" style="text-align:center">
                 <asp:HyperLink ID="linkRestricoes" NavigateUrl="~/MonRestricoes.aspx" runat="server">
	                 Restrições
	             </asp:HyperLink>    
	              </td>
	            </tr>
            </table>    
            <br />
          </center>  
        </div>
    </td>
</tr>
</table>
        <table style="width:100%">
          <tr>
            <td style="width:50%">
                <b>Projeto:</b> <asp:Label ID="lblfiltroprojeto" runat="server"></asp:Label><br />
                <b>Área de Resultado:</b> <asp:Label ID="lblfiltroarearesultado" runat="server"></asp:Label><br />
                <b>Entidade Responsável:</b> <asp:Label ID="lblfiltroentidaderesp" runat="server"></asp:Label><br />
                <b>Entidade Parceira:</b> <asp:Label ID="lblfiltroentidadeparc" runat="server"></asp:Label><br />
	        </td>
            <td>
                <b>Fase:</b> <asp:Label ID="lblfiltrofase" runat="server"></asp:Label><br />
                <b>Projetos analisados:</b> 
                <asp:HyperLink ID="linkfiltroprojetos" NavigateUrl="~/MonProjetos.aspx" runat="server"></asp:HyperLink>
                <br />
                <b>Período analisado:</b> <asp:Label ID="lblfiltroperiodo" runat="server"></asp:Label><br />
                <b>Monitoramento gerado em:</b> <asp:Label ID="lblfiltrogerado" runat="server"></asp:Label><br />
            </td>
          </tr>
        </table>
</asp:Panel>


  <div class="clear"></div></div><div class="bucket_bottom"><span></span></div></div><br />  
  </ContentTemplate> 
  <Triggers>
    <asp:AsyncPostBackTrigger ControlID="btnShow" />
    <asp:PostBackTrigger ControlID="btnFiltrar" />
  </Triggers>  
  </asp:UpdatePanel>
</asp:Content>

