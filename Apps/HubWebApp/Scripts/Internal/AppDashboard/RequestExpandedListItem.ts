import { FormattedDate } from "XtiShared/FormattedDate";
import { RequestExpandedListItemViewModel } from "./RequestExpandedListItemViewModel";

export class RequestExpandedListItem {
    constructor(vm: RequestExpandedListItemViewModel, req: IAppRequestExpandedModel) {
        vm.groupName(req.GroupName);
        vm.actionName(req.ActionName);
        let timeStarted = new FormattedDate(req.TimeStarted).formatDateTime();
        vm.timeStarted(timeStarted);
        vm.userName(req.UserName);
    }
}