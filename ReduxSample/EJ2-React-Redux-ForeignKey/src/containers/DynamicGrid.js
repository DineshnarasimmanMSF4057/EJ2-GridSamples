import React from 'react'
import { connect } from 'react-redux'
import { filter } from '../actions'

import { GridComponent, Filter,ColumnsDirective, ColumnDirective,ForeignKey, Inject } from '@syncfusion/ej2-react-grids';


let DynamicGrid = ({ gridData, dispatch }) => {
    let input
    return (
        <div>
        
            <GridComponent dataSource={gridData.data} allowFiltering={true} height = {300}>
                <ColumnsDirective>
                <ColumnDirective field='OrderID' headerText='Order ID' width='120' textAlign='Right'  isPrimaryKey={true}></ColumnDirective>
                <ColumnDirective field='CustomerID' headerText='Customer Name' width='150' foreignKeyValue='ContactName' foreignKeyField='CustomerID' dataSource={gridData.customerData}></ColumnDirective>
                        <ColumnDirective field='Freight' headerText='Freight' width='150' format='C2' textAlign='Right' editType='numericedit'/>
                        <ColumnDirective field='ShipName' headerText='Ship Name' width='170'></ColumnDirective>
                        <ColumnDirective field='ShipCountry' headerText='Ship Country' width='150' editType='dropdownedit'></ColumnDirective>
                </ColumnsDirective>
                <Inject services={[Filter,ForeignKey]} />
            </GridComponent>
        </div>
    )
}

const stateToProps = state => {
    return {
        gridData: state.dynamicFilter
    }
}
DynamicGrid = connect(stateToProps)(DynamicGrid)

export default DynamicGrid