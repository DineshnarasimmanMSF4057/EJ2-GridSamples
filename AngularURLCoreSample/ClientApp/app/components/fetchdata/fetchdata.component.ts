import { Component, ViewChild, ViewEncapsulation, OnInit, Inject } from '@angular/core';
import { DatePicker } from '@syncfusion/ej2-calendars';
import { Column, EditSettingsModel, ToolbarItems, IEditCell } from '@syncfusion/ej2-angular-grids';
import { DataManager, WebApiAdaptor, UrlAdaptor, Query } from '@syncfusion/ej2-data';
import { SaveEventArgs, IRow } from '@syncfusion/ej2-grids';
import { DropDownList } from '@syncfusion/ej2-dropdowns';
import { GridComponent } from '@syncfusion/ej2-angular-grids';
import { InfiniteScrollService, ToolbarService } from '@syncfusion/ej2-angular-grids';
import { employeeData, customerData, orderData, orderDatas } from './datasource';
import { PageSettingsModel, InfiniteScrollSettingsModel } from '@syncfusion/ej2-angular-grids';

import {
    
    ODataAdaptor,
    Predicate,
    ReturnOption
} from "@syncfusion/ej2-data";

@Component({
    selector: 'fetchdata',
    templateUrl: './fetchdata.component.html',
    encapsulation: ViewEncapsulation.Emulated,
    providers: [InfiniteScrollService, ToolbarService]
})
export class FetchDataComponent {
    public data: any;
    public filterSettings:any
    public editSettings: Object;
    public toolbar: any;

    public options: PageSettingsModel;

  @ViewChild('grid')
    public grid: GridComponent;

   
    @ViewChild("detailgrid")
    detailGrid: GridComponent;
   

    ngOnInit(): void {
        this.toolbar = ['Add', 'Edit', 'Delete', 'Update', 'Cancel'];
        this.filterSettings={type:'Excel'}
        this.editSettings = { allowEditing: true, allowAdding: true, allowDeleting: true, mode: 'Normal' };
 
        this.options = { pageSize: 50 };
   
  
        
    this.data = new DataManager({
        url: 'Home/UrlDatasource',
        updateUrl: 'Home/Update',
        insertUrl: 'Home/Insert',
        removeUrl: 'Home/Delete',
        adaptor: new UrlAdaptor
    });
       
       
    }
   
  
  

}

