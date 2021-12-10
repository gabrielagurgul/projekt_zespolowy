//
//  CategoryView.swift
//  TeamProject
//
//  Created by Grzegorz Gumieniak on 05/12/2021.
//

import SwiftUI

struct CategoryView: View {
	@Environment(\.colorScheme) var colorScheme
	let budgetType: BudgetType
	var body: some View {
		VStack {
			Text(budgetType.type)
				.font(.largeTitle)
			Text("🍕")
				.font(.system(size: 68))
			
		}
		.frame(width: 160, height: 160)
		.background(.regularMaterial)
		.mask(RoundedRectangle(cornerRadius: 8))
		.shadow(radius: 16)
	}
}

struct CategoryView_Previews: PreviewProvider {
	static var previews: some View {
		CategoryView(budgetType: BudgetType.budgetTypeMock)
	}
}
