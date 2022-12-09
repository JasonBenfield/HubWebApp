import { LinkComponent } from "@jasonbenfield/sharedwebapp/Components/LinkComponent";
import { ODataCell } from "@jasonbenfield/sharedwebapp/OData/ODataCell";
import { ODataColumn } from "@jasonbenfield/sharedwebapp/OData/ODataColumn";
import { HubAppApi } from "../../Lib/Api/HubAppApi";
import { InstallationDropdownView } from "./InstallationDropdownView";

export class InstallationDropdown extends ODataCell {
    constructor(
        hubApi: HubAppApi,
        readonly rowIndex: number,
        readonly column: ODataColumn,
        readonly record: any,
        protected readonly view: InstallationDropdownView
    ) {
        super(rowIndex, column, record, view);
        const installationID = record['InstallationID'];
        const requestLink = new LinkComponent(view.requestLink);
        requestLink.setHref(hubApi.Logs.Requests.getUrl({ SessionID: null, InstallationID: installationID }));
        const logEntryLink = new LinkComponent(view.logEntryLink);
        logEntryLink.setHref(hubApi.Logs.LogEntries.getUrl({ RequestID: null, InstallationID: installationID }));
    }
}