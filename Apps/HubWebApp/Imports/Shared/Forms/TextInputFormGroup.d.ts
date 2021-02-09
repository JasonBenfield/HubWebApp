import { BlockViewModel } from "../Html/BlockViewModel";
import { TextConstraintCollection } from "./ConstraintCollection";
import { ErrorList } from "./ErrorList";
import { InputFormGroup } from "./InputFormGroup";
export declare class TextInputFormGroup extends InputFormGroup<string> {
    constructor(prefix: string, name: string, vm?: BlockViewModel);
    readonly constraints: TextConstraintCollection;
    protected validateConstraints(fieldErrors: ErrorList): void;
}
