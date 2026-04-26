import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { CardView } from "@jasonbenfield/sharedwebapp/Views/Card";
import { GridListGroupView } from "@jasonbenfield/sharedwebapp/Views/ListGroup";
import { TextHeading3View } from "@jasonbenfield/sharedwebapp/Views/TextHeadings";
import { CommandStepListItemView } from "./CommandStepListItemView";

export class CommandStepListCardView extends CardView {
    readonly stepListView: GridListGroupView<CommandStepListItemView>;

    constructor(container: BasicComponentView) {
        super(container);
        const titleTextView = this.addCardHeader().addView(TextHeading3View);
        titleTextView.addCssName("card-title");
        titleTextView.setText("Command Steps");
        this.stepListView = this.addGridListGroup(CommandStepListItemView);
        CommandStepListItemView.setTemplateColumns(this.stepListView);
    }
}