import { Column } from "../Grid/Column";
import { LabelColumn } from "../Grid/LabelColumn";
import { Block } from "../Html/Block";
import { BlockViewModel } from "../Html/BlockViewModel";
import { InputGroup } from "../Html/InputGroup";
export declare class FormGroup extends Block implements IFormGroup {
    constructor(vm?: BlockViewModel);
    readonly captionColumn: LabelColumn;
    private readonly labelTextSpan;
    readonly inputGroup: InputGroup;
    private caption;
    getCaption(): string;
    setCaption(caption: string): void;
    readonly valueColumn: Column;
}
