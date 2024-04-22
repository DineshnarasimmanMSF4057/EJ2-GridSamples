import React, { Component } from 'react';
import {
    GridComponent,
    ColumnsDirective,
    ColumnDirective,
    Page,
    ColumnMenu,
    Sort,
    Filter,
    Resize,
    Inject, } from '@syncfusion/ej2-react-grids';
import { DataManager, WebApiAdaptor } from '@syncfusion/ej2-data';

import { Ajax } from '@syncfusion/ej2-base';

import { getStackedColumns } from '../util';
export class Home extends Component {
    static displayName = Home.name;
    gridInstance;
    config  = {
    OrderID: {
        clipmode: 'ellipsis',
        filterable: true,
        order: 24,
        orderable: true,
        resizable: true,
        show: true,
        showmenu: true,
        sortable: true,
        stacked: true,
        stackedHeader: 'test',
        aliased: true,
        aliasedHeader: 'checkcheck',
    },
    OrderDate: {
        clipmode: 'ellipsis',
        filterable: true,
        order: 25,
        orderable: true,
        resizable: true,
        show: true,
        format: 'dd/MM/yyyy',
        showmenu: true,
        sortable: true,
        stackedHeader: '',
    },
    Freight: {
        clipmode: 'ellipsis',
        filterable: true,
        order: 26,
        orderable: true,
        resizable: true,
        show: true,
        showmenu: true,
        sortable: true,
    },
    ShipCity: {
        clipmode: 'ellipsis',
        filterable: true,
        order: -111,
        orderable: true,
        resizable: true,
        show: true,
        showmenu: true,
        sortable: true,
        stacked: true,
        stackedHeader: 'Ship',
        freeze: true,
    },
    ShipCountry: {
        clipmode: 'ellipsis',
        filterable: true,
        order: 28,
        orderable: true,
        resizable: true,
        show: true,
        showmenu: true,
        sortable: true,
        stacked: true,
        stackedHeader: 'Ship',
    },
    };
    directives = [
        'OrderID',
        'OrderDate',
        'ShipCity',
        'ShipCountry',
        'Freight',
    ];

    stackedColumns = getStackedColumns(this.directives, this.config);
    constructor(props) {
        super(props);
        this.data =  new DataManager({
            url: 'api/Orders',
            adaptor: new WebApiAdaptor(),
        });
        this.editOptions = { allowEditing: true, allowAdding: true, allowDeleting: true, mode: 'Dialog' };
        this.toolbarOptions = ['Add', 'Edit', 'Delete', 'Update', 'Cancel'];
    }
    
     gridTemplate = (props) => {
        const src = "https://ej2.syncfusion.com/react/demos/src/grid/images/"+props.EmployeeID + '.png';
        return (<div className='image'>
            <img src={src} alt={props.EmployeeID} />
        </div>);
    };
  render () {
    return (
        <div>
            <GridComponent
                id="Grid"
                dataSource={this.data}
                allowSorting={true}
                allowPaging={true}
                allowFiltering={true}
                filterSettings={{ type: 'Menu' }}
                showColumnMenu={true}
                allowResizing={true}
                ref={(grid) => (this.gridInstance = grid)}
            >
                <ColumnsDirective>
                    {this.stackedColumns.map((stackedColumn, i) => {
                        return (
                            <ColumnDirective
                                field={stackedColumn.field}
                                key={`directive--${i}`}
                                format={stackedColumn.format}
                                headerText={stackedColumn.headerText}
                                columns={stackedColumn.columns}
                            />
                        );
                    })}
                </ColumnsDirective>
                <Inject services={[Page, ColumnMenu, Sort, Filter, Resize]} />
            </GridComponent>
      </div>
    );
  }
}
