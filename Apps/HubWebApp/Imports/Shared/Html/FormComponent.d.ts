import { ButtonCommandItem } from "../Command/ButtonCommandItem";
import { AggregateComponent } from "./AggregateComponent";
import { FormComponentViewModel } from "./FormComponentViewModel";
import { HtmlContainerComponent } from "./HtmlContainerComponent";
export declare class FormComponent extends HtmlContainerComponent {
    constructor(vm?: FormComponentViewModel);
    readonly vm: FormComponentViewModel;
    readonly submitted: import("../Events").DefaultEventHandler<any>;
    useDefaultSubmit(): void;
    readonly content: AggregateComponent;
    clearAutocomplete(): void;
    setAutocompleteOff(): void;
    setAutocompleteNewPassword(): void;
    private setAutocomplete;
    setAction(action: string): void;
    setMethod(method: string): void;
    addOffscreenSubmit(): ButtonCommandItem;
}
