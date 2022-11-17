<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ASAP.Assets.WebApp._Default" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="/Kendo/styles/kendo.common.min.css" rel="stylesheet" />
    <link href="/Kendo/styles/kendo.default.min.css" rel="stylesheet" />
    <link href="/Kendo/styles/kendo.default.mobile.min.css" rel="stylesheet" />
    <script src="/Kendo/js/jquery.min.js"></script>
    <script src="/Kendo/js/kendo.all.min.js"></script>
    
    <script src="//cdnjs.cloudflare.com/ajax/libs/jszip/2.4.0/jszip.min.js"></script>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <div id="grid"></div>
        </div>
    </div>
    
   


</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="FooterContent" runat="server">
    <script>
        $(document).ready(function () {
            var crudServiceBaseUrl = "https://localhost:44347/api/assets",
                dataSource = new kendo.data.DataSource({
                    transport: {
                        read: {
                            url: crudServiceBaseUrl,
                            dataType: "json"
                        },
                        update: {
                            url: crudServiceBaseUrl + "/detailproducts/Update",
                            dataType: "json"
                        },
                        destroy: {
                            url: crudServiceBaseUrl + "/detailproducts/Destroy",
                            dataType: "json"
                        },
                        parameterMap: function (options, operation) {
                            if (operation !== "read" && options.models) {
                                return { models: kendo.stringify(options.models) };
                            }
                        }
                    },
                    batch: true,
                    pageSize: 20,
                    autoSync: true,
                    schema: {
                        model: {
                            id: "Id",
                            fields: {
                                AssetNo: { editable: false, nullable: true },
                                AssetName: { type: "string", editable: false },
                                ModelNo: { type: "string", editable: false },
                                Vendor: { type: "string", editable: false },
                                Description: { type: "string" },
                                
                            }
                        }
                    }
                });

            $("#grid").kendoGrid({
                dataSource: dataSource,
                columnMenu: {
                    filterable: false
                },
                height: 680,
                editable: "incell",
                pageable: true,
                sortable: true,
                navigatable: true,
                resizable: true,
                reorderable: true,
                groupable: true,
                filterable: true,
                dataBound: onDataBound,
                toolbar: ["excel", "pdf", "search"],
                columns: [{
                    selectable: true,
                    width: 75,
                    attributes: {
                        "class": "checkbox-align",
                    },
                    headerAttributes: {
                        "class": "checkbox-align",
                    }
                }, {
                    field: "AssetNo",
                    title: "Asset #",
                },  {
                    field: "AssetName",
                    title: "Asset Name",
                },{
                    field: "ModelNo",
                    title: "Model #",
                },{
                    field: "Vendor",
                    title: "Vendor",
                },{
                    field: "Description",
                    title: "Description",
                },
                
                { command: "destroy", title: "&nbsp;", width: 120 }],
            });
        });

        function onDataBound(e) {
            var grid = this;
            grid.table.find("tr").each(function () {
                var dataItem = grid.dataItem(this);
                var themeColor = dataItem.Discontinued ? 'success' : 'error';
                var text = dataItem.Discontinued ? 'available' : 'not available';

                $(this).find(".badgeTemplate").kendoBadge({
                    themeColor: themeColor,
                    text: text,
                });

                $(this).find(".rating").kendoRating({
                    min: 1,
                    max: 5,
                    label: false,
                    selection: "continuous"
                });

                $(this).find(".sparkline-chart").kendoSparkline({
                    legend: {
                        visible: false
                    },
                    data: [dataItem.TargetSales],
                    type: "bar",
                    chartArea: {
                        margin: 0,
                        width: 180,
                        background: "transparent"
                    },
                    seriesDefaults: {
                        labels: {
                            visible: true,
                            format: '{0}%',
                            background: 'none'
                        }
                    },
                    categoryAxis: {
                        majorGridLines: {
                            visible: false
                        },
                        majorTicks: {
                            visible: false
                        }
                    },
                    valueAxis: {
                        type: "numeric",
                        min: 0,
                        max: 130,
                        visible: false,
                        labels: {
                            visible: false
                        },
                        minorTicks: { visible: false },
                        majorGridLines: { visible: false }
                    },
                    tooltip: {
                        visible: false
                    }
                });

                kendo.bind($(this), dataItem);
            });
        }

        
    </script>

</asp:Content>
