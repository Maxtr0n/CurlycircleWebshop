export interface TableColumn {
    name: string;
    dataField: string;
    dataType?: 'date' | 'number';
    format?: string;
    alignment?: string;
}