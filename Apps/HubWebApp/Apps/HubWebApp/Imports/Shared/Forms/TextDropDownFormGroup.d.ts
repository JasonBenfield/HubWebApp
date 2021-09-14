import { BlockViewModel } from "../Html/BlockViewModel";
import { TextConstraintCollection } from "./ConstraintCollection";
import { DropDownFormGroup } from "./DropDownFormGroup";
import { ErrorList } from "./ErrorList";
export declare class TextDropDownFormGroup extends DropDownFormGroup<string> {
    constructor(prefix: string, name: string, vm?: BlockViewModel);
    readonly constraints: TextConstraintCollection;
    protected validateConstraints(fieldErrors: ErrorList): void;
}
