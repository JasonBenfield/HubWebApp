import { Row } from "XtiShared/Grid/Row";
import { BlockViewModel } from "XtiShared/Html/BlockViewModel";
import { TextSpan } from "XtiShared/Html/TextSpan";
import { TextCss } from "XtiShared/TextCss";
import { ColumnCss } from "XtiShared/ColumnCss";
import { ContextualClass } from "XtiShared/ContextualClass";
import { FaIcon } from "XtiShared/FaIcon";

export class RoleAccessListItem extends Row {
    constructor(accessItem: IRoleAccessItem, vm: BlockViewModel = new BlockViewModel()) {
        super(vm);
        this.addColumn()
            .configure(c => c.setColumnCss(ColumnCss.xs('auto')))
            .addContent(new FaIcon(accessItem.isAllowed ? 'thumbs-up' : 'thumbs-down'))
            .configure(icon => {
                icon.regularStyle();
                icon.makeFixedWidth();
                icon.addCssFrom(
                    new TextCss().context(
                        accessItem.isAllowed
                            ? ContextualClass.success
                            : ContextualClass.danger
                    ).cssClass()
                );
            });
        this.addColumn()
            .addContent(new TextSpan(accessItem.role.Name));
    }
}