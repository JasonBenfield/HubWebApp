import { BlockViewModel } from "../Html/BlockViewModel";
import { DateConstraintCollection } from "./ConstraintCollection";
import { ErrorList } from "./ErrorList";
import { InputFormGroup } from "./InputFormGroup";
export declare class DateInputFormGroup extends InputFormGroup<Date> {
    constructor(prefix: string, name: string, vm?: BlockViewModel);
    readonly constraints: DateConstraintCollection;
    protected validateConstraints(fieldErrors: ErrorList): void;
}
