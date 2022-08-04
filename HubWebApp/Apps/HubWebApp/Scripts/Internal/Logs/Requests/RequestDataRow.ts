import { ContextualClass } from "@jasonbenfield/sharedwebapp/ContextualClass";
import { ODataColumn } from "@jasonbenfield/sharedwebapp/OData/ODataColumn";
import { ODataRow } from "@jasonbenfield/sharedwebapp/OData/ODataRow";
import { Queryable } from "@jasonbenfield/sharedwebapp/OData/Types";
import { GridRowView } from "@jasonbenfield/sharedwebapp/Views/Grid";

export class RequestDataRow extends ODataRow {
    constructor(rowIndex: number, columns: ODataColumn[], record: Queryable<IExpandedRequest>, view: GridRowView) {
        super(rowIndex, columns, record, view);
        if (!record.Succeeded) {
            view.setContext(ContextualClass.danger);
        }
    }
}