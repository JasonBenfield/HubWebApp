import { BlockViewModel } from "../Html/BlockViewModel";
import { ConstraintCollection } from "./ConstraintCollection";
import { DropDownFormGroup } from "./DropDownFormGroup";
import { ErrorList } from "./ErrorList";
export declare class BooleanDropDownFormGroup extends DropDownFormGroup<boolean> {
    constructor(prefix: string, name: string, vm?: BlockViewModel);
    readonly constraints: ConstraintCollection;
    protected validateConstraints(fieldErrors: ErrorList): void;
}
