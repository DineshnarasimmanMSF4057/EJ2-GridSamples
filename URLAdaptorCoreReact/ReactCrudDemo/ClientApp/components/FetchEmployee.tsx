import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { Link, NavLink } from 'react-router-dom';
import { GridComponent, ColumnDirective, ColumnsDirective, Grid, Group, Inject, Page, Edit, Toolbar } from '../../node_modules/@syncfusion/ej2-react-grids';
import { DataManager, UrlAdaptor } from '../../node_modules/@syncfusion/ej2-data';

interface FetchEmployeeDataState {
    empList: EmployeeData[];
    loading: boolean;
    data:any
}

export class FetchEmployee extends React.Component<RouteComponentProps<{}>, FetchEmployeeDataState> {
    public toolbarOptions: any = ['Add', 'Delete', 'Update', 'Cancel'];
    public editSettings: any = { allowEditing: true, allowAdding: true, allowDeleting: true, mode: 'Normal' };
    public pageOptions: any = {
        pageSize: 10
    };
    constructor(props) {
        super(props);       
    }
    public onload(args: any) {        
        let dm: any = new DataManager({
            url: "/Home/UrlDatasource",
            adaptor: new UrlAdaptor(),
            insertUrl: "/Home/Insert",
            updateUrl: "/Home/Update",
            removeUrl:"/Home/Remove"
        });
        (this as any).grid.dataSource = dm;
    }

    public render() {
        return (<div className='control-section'>
            <GridComponent ref={g => (this as any).grid = g} allowPaging={true} pageSettings={this.pageOptions} load={this.onload.bind(this)} editSettings={this.editSettings} toolbar={this.toolbarOptions} >
                <ColumnsDirective>
                    <ColumnDirective field='OrderID' headerText='Order ID' isPrimaryKey={true} width='120' textAlign='Right'></ColumnDirective>
                    <ColumnDirective field='CustomerID' headerText='CustomerID' width='150'></ColumnDirective>
                    <ColumnDirective field='Freight' headerText='Freight' format="C2" width='120' textAlign='Right' />
                    <ColumnDirective field='EmployeeID' headerText='Employee ID' type='number' editType='numericedit' width='150'></ColumnDirective>
                </ColumnsDirective>
                <Inject services={[Page, Edit, Toolbar]} />a
            </GridComponent>
        </div>)
    }       
}

export class EmployeeData {
    employeeId: number = 0;
    name: string = "";
    gender: string = "";
    city: string = "";
    department: string = "";
} 