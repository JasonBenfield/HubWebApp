import { FaIcon } from "../FaIcon";
import { BlockViewModel } from "../Html/BlockViewModel";
import { HtmlComponent } from "../Html/HtmlComponent";
import { Column } from "./Column";
import { LabelColumn } from "./LabelColumn";
export declare class Row extends HtmlComponent {
    constructor(vm?: BlockViewModel);
    protected readonly vm: BlockViewModel;
    private readonly block;
    readonly columns: IColumn[];
    addIconColumn(name: string, config?: (icon: FaIcon) => void): Column;
    addTextColumn(text?: string): Column;
    addColumn(): Column;
    addLabelColumn(): LabelColumn;
}
