import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { TableColumn } from './table.models';

@Component({
    selector: 'app-table',
    templateUrl: './table.component.html',
    styleUrls: ['./table.component.scss']
})
export class TableComponent implements OnInit {
    @Input() dataSource: any[];
    @Input() columns: TableColumn[];
    @Input() rowsClickable = true;
    @Input() displayedColumns?: string[];
    @Output() rowClicked = new EventEmitter<any>();

    constructor(private readonly datePipe: DatePipe) { }

    ngOnInit(): void {
        this.displayedColumns ??= this.columns.map(col => col.name);
    }

    onRowClicked(rowData: any): void {
        if (this.rowsClickable) {
            this.rowClicked.emit(rowData);
        }
    }

    formatCell(rowData: any, column: TableColumn): any {
        const value = rowData[column.dataField];
        if (column.dataType === 'date' && column.format) {
            return this.datePipe.transform(rowData[column.dataField], column.format);
        }
        return value;
    }

}
