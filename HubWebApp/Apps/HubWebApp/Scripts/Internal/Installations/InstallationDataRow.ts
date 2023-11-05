import { ContextualClass } from "@jasonbenfield/sharedwebapp/ContextualClass";
import { ODataColumn } from "@jasonbenfield/sharedwebapp/OData/ODataColumn";
import { ODataRow } from "@jasonbenfield/sharedwebapp/OData/ODataRow";
import { Queryable } from "@jasonbenfield/sharedwebapp/OData/Types";
import { GridRowView } from "@jasonbenfield/sharedwebapp/Views/Grid";
import { InstallStatus } from '../../Lib/Http/InstallStatus';

export class InstallationDataRow extends ODataRow {
    constructor(rowIndex: number, columns: ODataColumn[], record: Queryable<IExpandedInstallation>, view: GridRowView) {
        super(rowIndex, columns, record, view);
        const status = InstallStatus.values.value(record.InstallationStatusDisplayText);
        if (InstallStatus.values.Deleted.equals(status)) {
            view.setContext(ContextualClass.danger);
        }
        else if (InstallStatus.values.DeletePending.equals(status) || InstallStatus.values.DeleteStarted.equals(status)) {
            view.setContext(ContextualClass.warning);
        }
    }
}