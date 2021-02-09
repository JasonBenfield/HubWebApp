import { ColumnCss } from "../ColumnCss";
import { Label } from "../Html/Label";
import { LabelViewModel } from "../Html/LabelViewModel";
export declare class LabelColumn extends Label {
    constructor(vm?: LabelViewModel);
    protected readonly vm: LabelViewModel;
    private columnCss;
    setColumnCss(columnCss: ColumnCss): void;
}
