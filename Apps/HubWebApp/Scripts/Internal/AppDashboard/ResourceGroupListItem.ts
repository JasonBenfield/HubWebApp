import { Row } from "XtiShared/Grid/Row";
import { BlockViewModel } from "XtiShared/Html/BlockViewModel";
import { TextSpan } from "XtiShared/Html/TextSpan";

export class ResourceGroupListItem extends Row {
    constructor(rg: IResourceGroupModel, vm: BlockViewModel = new BlockViewModel()) {
        super(vm);
        this.addColumn()
            .addContent(new TextSpan(rg.Name));
    }
}