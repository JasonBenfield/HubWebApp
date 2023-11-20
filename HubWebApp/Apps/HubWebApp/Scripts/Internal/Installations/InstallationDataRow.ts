import { ContextualClass } from "@jasonbenfield/sharedwebapp/ContextualClass";
import { ODataColumn } from "@jasonbenfield/sharedwebapp/OData/ODataColumn";
import { ODataLinkRow } from "@jasonbenfield/sharedwebapp/OData/ODataLinkRow";
import { Queryable } from "@jasonbenfield/sharedwebapp/OData/Types";
import { LinkGridRowView } from "@jasonbenfield/sharedwebapp/Views/Grid";
import { InstallStatus } from '../../Lib/Http/InstallStatus';
import { HubAppClient } from "../../Lib/Http/HubAppClient";

export class InstallationDataRow extends ODataLinkRow {
    constructor(hubClient: HubAppClient, rowIndex: number, columns: ODataColumn[], record: Queryable<IExpandedInstallation>, view: LinkGridRowView) {
        super(rowIndex, columns, record, view);
        const status = InstallStatus.values.value(record.InstallationStatus);
        if (InstallStatus.values.Deleted.equals(status)) {
            view.setContext(ContextualClass.danger);
        }
        else if (InstallStatus.values.DeletePending.equals(status) || InstallStatus.values.DeleteStarted.equals(status)) {
            view.setContext(ContextualClass.warning);
        }
        const installationID: number = record['InstallationID'];
        this.setHref(hubClient.Installations.Installation.getUrl({ InstallationID: installationID }));
    }
}