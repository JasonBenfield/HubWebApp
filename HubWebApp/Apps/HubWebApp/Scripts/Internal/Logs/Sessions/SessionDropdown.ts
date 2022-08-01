import { LinkComponent } from "../../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Components/LinkComponent";
import { ODataCell } from "../../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/OData/ODataCell";
import { ODataColumn } from "../../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/OData/ODataColumn";
import { HubAppApi } from "../../../Lib/Api/HubAppApi";
import { SessionDropdownView } from "./SessionDropdownView";

export class SessionDropdown extends ODataCell {
    constructor(
        hubApi: HubAppApi,
        readonly rowIndex: number,
        readonly column: ODataColumn,
        readonly record: any,
        protected readonly view: SessionDropdownView
    ) {
        super(rowIndex, column, record, view);
        const requestLink = new LinkComponent(view.requestLink);
        const sessionID = record['SessionID'];
        requestLink.setHref(hubApi.Logs.Requests.getUrl({ SessionID: sessionID }));
    }
}