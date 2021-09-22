import { Row } from "XtiShared/Grid/Row";
import { BlockViewModel } from "XtiShared/Html/BlockViewModel";
import { TextSpan } from "XtiShared/Html/TextSpan";
import { TextCss } from "XtiShared/TextCss";
import { ColumnCss } from "XtiShared/ColumnCss";

export class ModifierListItem extends Row {
    constructor(modifier: IModifierModel, vm: BlockViewModel = new BlockViewModel()) {
        super(vm);
        this.addColumn()
            .configure(c => {
                c.setColumnCss(ColumnCss.xs(4));
                c.addCssFrom(new TextCss().truncate().cssClass());
            })
            .addContent(new TextSpan(modifier.ModKey))
            .configure(ts => ts.setTitleFromText());
        this.addColumn()
            .configure(c => c.addCssFrom(new TextCss().truncate().cssClass()))
            .addContent(new TextSpan(modifier.DisplayText))
            .configure(ts => ts.setTitleFromText());
    }
}