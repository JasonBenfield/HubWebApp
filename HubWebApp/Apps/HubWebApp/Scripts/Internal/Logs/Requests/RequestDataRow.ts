import { ContextualClass } from "@jasonbenfield/sharedwebapp/ContextualClass";
import { ODataColumn } from "@jasonbenfield/sharedwebapp/OData/ODataColumn";
import { ODataLinkRow } from "@jasonbenfield/sharedwebapp/OData/ODataLinkRow";
import { Queryable } from "@jasonbenfield/sharedwebapp/OData/Types";
import { LinkGridRowView } from "@jasonbenfield/sharedwebapp/Views/Grid";
import { HubAppClient } from "../../../Lib/Http/HubAppClient";

export class RequestDataRow extends ODataLinkRow {
    constructor(hubClient: HubAppClient, rowIndex: number, columns: ODataColumn[], record: Queryable<IExpandedRequest>, view: LinkGridRowView) {
        super(rowIndex, columns, record, view);
        const requestID: number = record.RequestID;
        this.setHref(hubClient.Logs.AppRequest.getUrl({ RequestID: requestID }));
        if (!record.Succeeded) {
            view.setContext(ContextualClass.danger);
        }
    }
}