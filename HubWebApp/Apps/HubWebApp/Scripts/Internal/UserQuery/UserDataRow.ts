import { ContextualClass } from "@jasonbenfield/sharedwebapp/ContextualClass";
import { ODataColumn } from "@jasonbenfield/sharedwebapp/OData/ODataColumn";
import { ODataLinkRow } from "@jasonbenfield/sharedwebapp/OData/ODataLinkRow";
import { Queryable } from "@jasonbenfield/sharedwebapp/OData/Types";
import { LinkGridRowView } from "@jasonbenfield/sharedwebapp/Views/Grid";
import { HubAppClient } from "../../Lib/Http/HubAppClient";

export class UserDataRow extends ODataLinkRow {
    constructor(hubClient: HubAppClient, rowIndex: number, columns: ODataColumn[], record: Queryable<IExpandedUser>, view: LinkGridRowView) {
        super(rowIndex, columns, record, view);
        const userID: number = record['UserID'];
        let userGroupName: string = record['UserGroupName'];
        userGroupName = userGroupName.replace(/\s+/g, '');
        this.setHref(
            hubClient.Users.Index.getModifierUrl(
                userGroupName, {
                UserID: userID,
                ReturnTo: userGroupName
            })
        );
        if (!record.IsActive) {
            view.setContext(ContextualClass.danger);
        }
    }
}