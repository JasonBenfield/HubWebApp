import { FormattedDate } from "XtiShared/FormattedDate";
import { Row } from "XtiShared/Grid/Row";
import { BlockViewModel } from "XtiShared/Html/BlockViewModel";
import { TextSpan } from "XtiShared/Html/TextSpan";
import { TextCss } from "XtiShared/TextCss";

export class EventListItem extends Row {
    constructor(evt: IAppEventModel, vm: BlockViewModel = new BlockViewModel()) {
        super(vm);
        this.addColumn()
            .addContent(new TextSpan(new FormattedDate(evt.TimeOccurred).formatDateTime()));
        this.addColumn()
            .addContent(new TextSpan(evt.Severity.DisplayText));
        this.addColumn()
            .configure(c => c.addCssFrom(new TextCss().truncate().cssClass()))
            .addContent(new TextSpan(evt.Caption))
            .configure(ts => ts.setTitle(evt.Caption));
        this.addColumn()
            .configure(c => c.addCssFrom(new TextCss().truncate().cssClass()))
            .addContent(new TextSpan(evt.Message))
            .configure(ts => ts.setTitle(evt.Message));
    }
}