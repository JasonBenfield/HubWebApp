import { FormattedDate } from "XtiShared/FormattedDate";
import { Row } from "XtiShared/Grid/Row";
import { BlockViewModel } from "XtiShared/Html/BlockViewModel";
import { TextSpan } from "XtiShared/Html/TextSpan";

export class RequestExpandedListItem extends Row {
    constructor(req: IAppRequestExpandedModel, vm: BlockViewModel = new BlockViewModel()) {
        super(vm);
        let timeStarted = new FormattedDate(req.TimeStarted).formatDateTime();
        this.addColumn()
            .addContent(new TextSpan(timeStarted));
        this.addColumn()
            .addContent(new TextSpan(req.GroupName));
        this.addColumn()
            .addContent(new TextSpan(req.ActionName));
        this.addColumn()
            .addContent(new TextSpan(req.UserName));
    }
}