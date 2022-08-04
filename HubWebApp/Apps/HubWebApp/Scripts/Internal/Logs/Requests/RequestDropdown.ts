import { LinkComponent } from "@jasonbenfield/sharedwebapp/Components/LinkComponent";
import { ODataCell } from "@jasonbenfield/sharedwebapp/OData/ODataCell";
import { ODataColumn } from "@jasonbenfield/sharedwebapp/OData/ODataColumn";
import { HubAppApi } from "../../../Lib/Api/HubAppApi";
import { RequestDropdownView } from "./RequestDropdownView";

export class RequestDropdown extends ODataCell {
    constructor(
        hubApi: HubAppApi,
        readonly rowIndex: number,
        readonly column: ODataColumn,
        readonly record: any,
        protected readonly view: RequestDropdownView
    ) {
        super(rowIndex, column, record, view);
        const logEntryLink = new LinkComponent(view.logEntryLink);
        const requestID = record['RequestID'];
        logEntryLink.setHref(hubApi.Logs.LogEntries.getUrl({ RequestID: requestID }));
    }
}