import { Component, ViewChild } from '@angular/core';
import {
  GridComponent,
  ToolbarItems,
  EditSettingsModel,
  FilterSettingsModel,
} from '@syncfusion/ej2-angular-grids';
import { DataManager, ODataV4Adaptor } from '@syncfusion/ej2-data';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
})
export class AppComponent {
  @ViewChild('grid')
  public grid?: GridComponent;
  public data?: DataManager;
  public editSettings?: EditSettingsModel;
  public toolbar?: ToolbarItems[];
  public filterSettings?: FilterSettingsModel;

  ngOnInit(): void {
    this.data = new DataManager({
      url: 'https://localhost:44337/odata/Orders',
      adaptor: new ODataV4Adaptor(),
    });

    this.editSettings = {
      allowEditing: true,
      allowAdding: true,
      allowDeleting: true,
      mode: 'Normal',
    };
    this.toolbar = ['Add', 'Edit', 'Delete', 'Update', 'Cancel'];
    this.filterSettings = { type: 'Menu' };
  }
}
